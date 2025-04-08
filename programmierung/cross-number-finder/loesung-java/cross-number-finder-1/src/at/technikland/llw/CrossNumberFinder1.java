package at.technikland.llw;

import java.io.IOException;
import java.io.RandomAccessFile;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Scanner;

public class CrossNumberFinder1 {

	static final int LIMIT = 100;

	public static void main(String[] args) {
		int lineNumber = 0;
		String fileName = getFileName(args);
		try (RandomAccessFile file = new RandomAccessFile(fileName, "r")) {
			String line;
			while ((line = file.readLine()) != null) {
				lineNumber++;
				int crossNumber = calculateCrossNumber(line);
				if (crossNumber > LIMIT) {
					System.out.println("Zeile " + lineNumber + ": " + crossNumber);
				}
			}
			file.close();
		} catch (IOException e) {
			System.out.println("Failed to read file " + fileName);
		}
	}

	// Calculates the cross number based on one single line by splitting into each
	// character and summing up the numbers
	static int calculateCrossNumber(String line) {
		int crossNumber = 0;
		String[] numbers = line.split("");
		for (String number : numbers) {
			crossNumber += Integer.valueOf(number);
		}
		return crossNumber;
	}

	// gets the file name either from the arguments or from the user input
	@SuppressWarnings("resource")
	static String getFileName(String[] args) {
		if (args.length == 1 && Files.isRegularFile(Paths.get(args[0]))) {
			return args[0];
		}
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
}
