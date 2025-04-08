package at.technikland.llw;

import java.io.IOException;
import java.io.RandomAccessFile;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Scanner;

public class NumberFinder2 {

	public static final String NUMBER_TO_FIND = "777";
	
	public static void main(String[] args) {
		int count = 0;
		String fileName = getFileName(args);
		try (RandomAccessFile file = new RandomAccessFile(fileName, "r")) {
			String line;
			int countInLine = 0;
			int lineNr = 0;
			while ((line = file.readLine()) != null) {
				lineNr++;
				countInLine = getCountInLine(line);
				count += countInLine;
				if (countInLine > 0) {
					System.out.println("Zeile " + lineNr + ": " + countInLine + "x");
				}
			}
			file.close();
			System.out.println("Summe: " +  count + "x");
		} catch (IOException e) {
			System.out.println("Fehler beim Lesen der Datei: " + fileName);
		}
	}

	static int getCountInLine(String line) {
		int count = 0;
		String[] chunks = line.split(" ");
		for (String chunk : chunks) {
			if (NUMBER_TO_FIND.equals(chunk)) {
				count++;
			}
		}
		return count;
	}

	// gets the file name either from the arguments or from the user input
	@SuppressWarnings("resource")
	static String getFileName(String[] args) {
		if (args.length == 1 && Files.isRegularFile(Paths.get(args[0]))) {
			return args[0];
		}
		Scanner scanner = new Scanner(System.in);
		String fileName = null;
		System.out.println("Bitte geben sie einen Pfad zu einer Datei an:");
		fileName = scanner.nextLine();
		while (!Files.isRegularFile(Paths.get(fileName))) {
			System.out.println("Datei " + fileName + " konnte nicht gefunden/gelesen werden.");
			fileName = scanner.nextLine();
		}
		return fileName;
	}
	
}
