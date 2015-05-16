# Sudoku Board Generator
Sudoku board generator for C#

## Usage

Compile the application, and use like so:

```
generator <workers> <puzzles> <output>
```

Where:
 - `workers` is the number of threads to use for processing.
 - `puzzles` is the number of puzzles you would like to generate.
 - `output` is the directory in which to write the puzzles.

## Another generator? Really?

This board generator was used in my [Aivoterveydeksi!](http://perrymitchell.net/portfolio/aivoterveydeksi/) application build for iOS. I used this utility to generate thousands of boards to bundle with the app. You can read about why [here](http://perrymitchell.net/article/building-sudoku-for-mobile-applications).

## Plans

This project may be revisited later for an overhaul, as it was never intended to be made public. I do have plans to release a NodeJS version as well, so that may take priority.