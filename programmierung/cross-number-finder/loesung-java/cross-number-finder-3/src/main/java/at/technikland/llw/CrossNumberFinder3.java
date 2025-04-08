package at.technikland.llw;

import java.io.IOException;
import java.io.RandomAccessFile;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Scanner;

// TODOS: 
// - Handle ResultSets that might not be as expected 
// - Handle cases where the limit is never reached in a line
// - More detailed exception handling 
// - Handle db location, e.g. per command line switch

public class CrossNumberFinder3 {

	private static final String CONNECTION_STRING = "jdbc:sqlite:number.db";
	static final int LIMIT = 50;

	public static void main(String[] args) {
		String operation = askUserForOperation();
		switch (operation) {
		case "1":
			processFile();
			break;
		case "2":
			showStatistic();
			break;
		case "3":
			System.out.println("Ende");
			break;
		}
	}

	// Calls all statistic methods
	private static void showStatistic() {
		try (Connection connection = getDbConnection()) {
			showFileWithSmallestLimit(connection);
			showAvarges(connection);
			showFileWithMostLine1Limit(connection);
		} catch (SQLException e) {
			System.err.println("Fehler beim Ausführen von SQL Statements");
			e.printStackTrace();
		}
	}

	// Finds the file where most amount of numbers needed to reach the limit in line 1
	// Print out the filename and the amount of numbers needed to reach the limit
	private static void showFileWithMostLine1Limit(Connection connection) throws SQLException {
		String sql = "select filepath, max(limit_reached_at) as max_limit_reached from files join lines on files.id = lines.file_id where lines.line_number=1 group by files.id order by max_limit_reached limit 1";
		ResultSet result = connection.createStatement().executeQuery(sql);
		System.out.println("Erreichen des Limits in Zeile 1:");
		if (result.next()) {
			String filePathWithMaxReachedInLine1 = result.getString(1);
			int limitReachedAt = result.getInt(2);
			System.out.println("\tDatei: " + filePathWithMaxReachedInLine1 + "; Limit erreicht bei " + limitReachedAt
					+ " Zeichen/Zahlen");
		}
		System.out.println();
		result.close();
	}

	// Finds the filename with the smallest amount of numbers needed to reach the limit
	// Prints out the filename and the amount
	private static void showFileWithSmallestLimit(Connection connection) throws SQLException {
		String sql = "select file_path, min(limit_reached_at) as min_limit_reached from files join lines on files.id = lines.file_id group by files.id order by min_limit_reached limit 1";
		ResultSet result = connection.createStatement().executeQuery(sql);
		System.out.println("Schnellstes Erreichen des Limits:");
		if (result.next()) {
			String filePathWithMinReachedAt = result.getString(1);
			int limitReachedAt = result.getInt(2);
			System.out.println("\tDatei: " + filePathWithMinReachedAt + "; Limit erreicht bei " + limitReachedAt
					+ " Zeichen/Zahlen");
		}
		System.out.println();
		result.close();
	}

	// Prints out the average amount of numbers needed to reach the limit of each file
	private static void showAvarges(Connection connection) throws SQLException {
		String sql = "select filepath, avg(limit_reached_at) as avg_limit_reached from files join lines on files.id = lines.file_id group by files.id order by avg_limit_reached";
		ResultSet result = connection.createStatement().executeQuery(sql);
		System.out.println("Durchschnitt pro Datei:");
		while (result.next()) {
			String filePath = result.getString(1);
			double avg = result.getDouble(2);
			System.out.println("\tDatei: " + filePath + "; Durchschnitt: " + avg);
		}
		System.out.println();
		result.close();
	}

	// Asks the user what he wants do do, kind of a menu
	private static String askUserForOperation() {
		System.out.println("Number-Finder-Tool");
		System.out.println();
		System.out.println("Funktionen:");
		System.out.println("1...Datei einlesen/parsen");
		System.out.println("2...Statistik anzeigen");
		System.out.println("3...Quit");
		System.out.println();
		System.out.println("Welche Funktion soll ausgeführt werden?");
		Scanner inScanner = new Scanner(System.in);
		String choise = inScanner.nextLine();
		while (!("1".equals(choise) || "2".equals(choise) || "3".equals(choise))) {
			System.out.println("Bitte wählen sie 1, 2 oder 3");
			choise = inScanner.nextLine();
		}
		return choise;
	}

	// Proceeds a file, prints out the findings and saves the values to the database
	private static void processFile() {
		int lineNumber = 0;
		int smallestLimitReachedAtLineNumber = 0;
		int smallestLimitReachedAt = Integer.MAX_VALUE;
		String fileName = getFileName();
		try (RandomAccessFile file = new RandomAccessFile(fileName, "r"); Connection connection = getDbConnection()) {
			String line;
			int fileId = insertFile(connection, fileName);
			while ((line = file.readLine()) != null) {
				lineNumber++;
				int limitReachedAt = calculateLimitReachedAt(line);
				System.out.println("Zeile " + lineNumber + ": " + limitReachedAt);
				insertLine(connection, fileId, lineNumber, limitReachedAt);
				if (limitReachedAt < smallestLimitReachedAt) {
					smallestLimitReachedAt = limitReachedAt;
					smallestLimitReachedAtLineNumber = lineNumber;
				}
			}
			System.out.println("Zeile " + smallestLimitReachedAtLineNumber + " ist die Zeile, die am schnellsten die "
					+ LIMIT + " erreicht hat");
			file.close();
		} catch (IOException e) {
			System.err.println("Failed to read file " + fileName);
		} catch (SQLException e) {
			System.err.println();
		}
	}

	// Creates the entry in the db for the specified line values
	private static void insertLine(Connection connection, int fileId, int lineNumber, int limitReachedAt)
			throws SQLException {
		PreparedStatement statement = connection
				.prepareStatement("insert into lines(file_id, line_number, limit_reached_at) values(?,?,?)");
		statement.setInt(1, fileId);
		statement.setInt(2, lineNumber);
		statement.setInt(3, limitReachedAt);
		statement.execute();
	}

	// Creates the entry in the files table for the specified file
	private static int insertFile(Connection connection, String fileName) throws SQLException {
		PreparedStatement statement = connection.prepareStatement("insert into files(filepath) values(?)");
		statement.setString(1, fileName);
		statement.execute();
		ResultSet rs = statement.getGeneratedKeys();
		rs.next();
		return rs.getInt(1);
	}

	// Returns at which position the cross number exceeds the limit within line or
	// -1 if the limit is not reached
	private static int calculateLimitReachedAt(String line) {
		int crossNumber = 0;
		int count = 0;
		String[] numbers = line.split("");
		for (String number : numbers) {
			count++;
			crossNumber += Integer.valueOf(number);
			if (crossNumber >= 50) {
				return count;
			}
		}
		return -1;
	}

	// gets the file name either from the arguments or from the user input
	private static String getFileName() {
		Scanner scanner = new Scanner(System.in);
		String fileName = null;
		System.out.println("Specify the path to the file that should be used:");
		fileName = scanner.nextLine();
		while (!Files.isRegularFile(Paths.get(fileName))) {
			System.out.println("File " + fileName + " could not be found, specify a valid path");
			fileName = scanner.nextLine();
		}
		return fileName;
	}

	// Connects to the database and returns the connection object
	private static Connection getDbConnection() throws SQLException {
		Connection connection = DriverManager.getConnection(CONNECTION_STRING);
		checkDb(connection);
		return connection;
	}

	// Checks the db and creates the table if they do not exist
	private static void checkDb(Connection connection) throws SQLException {
		connection.createStatement().execute("create table if not exists files(id integer primary key autoincrement, file_path varchar(255))");
		connection.createStatement().execute("create table if not exists lines(id integer primary key autoincrement, file_id integer, line_number integer, count_of_matches integer, foreign key(file_id) references files(id))");
	}

}
