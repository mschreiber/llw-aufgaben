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

public class NumberFinder3 {
	
	private static final String CONNECTION_STRING = "jdbc:sqlite:number.db";
	static final String NUMBER_TO_FIND = "777";

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

	private static void showStatistic() {
		try (Connection connection = getDbConnection()) {
			showFileWithMostMatches(connection);
			showFileWithMostMatchesInOneLine(connection);
			showFilesWith3MatchesInLine2(connection);
			showFilesWithMoreThan4Matches(connection);
		} catch (SQLException e) {
			System.err.println("Fehler beim Ausführen von SQL Statements");
			e.printStackTrace();
		}
	}

	private static void showFilesWithMoreThan4Matches(Connection connection) throws SQLException {
		String sql = "select file_path, sum(count_of_matches) as count_in_file from files join lines on files.id = lines.file_id group by files.id having count_in_file > 4";
		ResultSet result = connection.createStatement().executeQuery(sql);
		System.out.println("Folgende Datei(n) haben mehr als 4 Matches");
		while (result.next()) {
			String  filePath = result.getString(1);
			System.out.println("-" + filePath);
		}
		System.out.println();
		result.close();
	}

	private static void showFileWithMostMatches(Connection connection) throws SQLException {
		String sql = "select file_path, sum(count_of_matches) as max_count_of_matches from files join lines on files.id = lines.file_id group by files.id order by max_count_of_matches desc limit 1";
		ResultSet result = connection.createStatement().executeQuery(sql);
		if (result.next()) {
			String fileWithMaxMatches = result.getString(1);
			int matches = result.getInt(2);
			System.out.println("Datei mit den meisten Matches: " + fileWithMaxMatches + ": " + matches + " Matches");
		}
		System.out.println();
		result.close();
	}

	private static void showFileWithMostMatchesInOneLine(Connection connection) throws SQLException {
		String sql = "select file_path, count_of_matches, line_number from files join lines on files.id = lines.file_id order by count_of_matches desc limit 1";
		ResultSet result = connection.createStatement().executeQuery(sql);
		if (result.next()) {
			String filePathWithMinReachedAt = result.getString(1);
			int count = result.getInt(2);
			int lineNumber = result.getInt(3);
			System.out.printf("Datei mit den meisten Matches pro Zeile: %s,: %d Matches in Zeile %d", filePathWithMinReachedAt, count, lineNumber);
		}
		System.out.println();
		result.close();
	}

	private static void showFilesWith3MatchesInLine2(Connection connection) throws SQLException {
		String sql = "select file_path from files join lines on files.id = lines.file_id where line_number = 2 and count_of_matches = 3 limit 1";
		ResultSet result = connection.createStatement().executeQuery(sql);
		if (result.next()) {
			String filePath = result.getString(1);
			System.out.println("Datei:  \"" + filePath + "\" hat in Zeile 2, drei Matches");
		}
		System.out.println();
		result.close();
	}

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

	private static void processFile() {
		int lineNumber = 0;
		int count = 0;
		String fileName = getFileName();
		try (RandomAccessFile file = new RandomAccessFile(fileName, "r"); Connection connection = getDbConnection()) {
			String line;
			int fileId = insertFile(connection, fileName);
			int countInLine = 0;
			while ((line = file.readLine()) != null) {
				lineNumber++;
				countInLine = getCountInLine(line);
				if (countInLine > 0) {
					System.out.println("Zeile " + lineNumber + ": " + countInLine + "x");
					insertLine(connection, fileId, lineNumber, countInLine);
				}
			}
			System.out.println("Summe: " +  count + "x");
			file.close();
		} catch (IOException e) {
			System.err.println("Failed to read file " + fileName);
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}

	private static void insertLine(Connection connection, int fileId, int lineNumber, int count)
			throws SQLException {
		PreparedStatement statement = connection
				.prepareStatement("insert into lines(file_id, line_number, count_of_matches) values(?,?,?)");
		statement.setInt(1, fileId);
		statement.setInt(2, lineNumber);
		statement.setInt(3, count);
		statement.execute();
	}

	private static int insertFile(Connection connection, String fileName) throws SQLException {
		PreparedStatement statement = connection.prepareStatement("insert into files(file_path) values(?)");
		statement.setString(1, fileName);
		statement.execute();
		ResultSet rs = statement.getGeneratedKeys();
		rs.next();
		return rs.getInt(1);
	}

	private static int getCountInLine(String line) {
		int count = 0;
		String[] chunks = line.split(" ");
		for (String chunk : chunks) {
			if (NUMBER_TO_FIND.equals(chunk)) {
				count++;
			}
		}
		return count;
	}

	private static String getFileName() {
		Scanner scanner = new Scanner(System.in);
		String fileName = null;
		System.out.println("Welche Datei soll eingelesen werden? (Pfad inkl. Dateiname):");
		fileName = scanner.nextLine();
		while (!Files.isRegularFile(Paths.get(fileName))) {
			System.out.println("Datei " + fileName + " konnte nicht gelesen/gefunden werden, geben sie eine Datei inkl. Pfad an!");
			fileName = scanner.nextLine();
		}
		return fileName;

	}

	private static Connection getDbConnection() throws SQLException {
		Connection connection = DriverManager.getConnection(CONNECTION_STRING);
		checkDb(connection);
		return connection;
	}

	private static void checkDb(Connection connection) throws SQLException {
		connection.createStatement().execute("create table if not exists files(id integer primary key autoincrement, file_path varchar(255))");
		connection.createStatement().execute("create table if not exists lines(id integer primary key autoincrement, file_id integer, line_number integer, count_of_matches integer, foreign key(file_id) references files(id))");
	}

}
