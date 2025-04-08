package at.technikland.llw;

import java.io.IOException;
import java.io.RandomAccessFile;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.util.Scanner;

public class CrossNumberFinder2 {

	static final int LIMIT = 50;

	public static void main(String[] args) {
		int lineNumber = 0;
		int smallestLimitReachedAtLineNumber = 0;
		int smallestLimitReachedAt = Integer.MAX_VALUE;
		String fileName = getFileName(args);
		try (RandomAccessFile file = new RandomAccessFile(fileName, "r")) {
			String line;
			while ((line = file.readLine()) != null) {
				lineNumber++;
				int limitReachedAt = calculateLimitReachedAt(line);
				System.out.println("Zeile " + lineNumber + ": " + limitReachedAt);
				if (limitReachedAt < smallestLimitReachedAt) {
					smallestLimitReachedAt = limitReachedAt;
					smallestLimitReachedAtLineNumber = lineNumber;
				}
			}
			System.out.println("Zeile " + smallestLimitReachedAtLineNumber + " ist die Zeile, die am schnellsten die "
					+ LIMIT + " erreicht hat");
			file.close();
		} catch (IOException e) {
			System.out.println("Failed to read file " + fileName);
		}
	}

	// Returns at which position the cross number exceeds the limit within line or
	// -1 if the limit is not reached
	static int calculateLimitReachedAt(String line) {
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
