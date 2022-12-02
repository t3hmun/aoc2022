/* This is the first Rust I've ever written so maximum qualification and annotations.
* Random notes:
* - Reading the first 5 chapters of the Rust Book was essential
* - The types for strings
    - Not magical like they are in C# and JS.
    - Not ASCII char array like in C
    - UTF-8 byte array, len might be different to number of letters
    - Different types for fix len strings (str) and growable strings (Strings)
*/

fn main() {
    let data: String = std::fs::read_to_string("../day-01-data.txt").expect("Failed to read file.");
    for_loops(&data);
    map_reduce(&data);
}

/// This is my instinctive way to do it but understanding types returned by each function isn't obvious.
fn map_reduce(data: &String) {
    let elves: std::str::Split<&str> = data.split("\n\n");
    //for elf in elves { /* The iterated elf is &str here */ }

    //let elves = elves.map(|elf| elf.parse::<u32>());
    //for elf in elves { /* The iterated elf is Result<u32, ParseIntError> here */ }

    // The type of below is "impl Iterator<Item = u32>", seems to be an inference only type?
    //let elves = elves.map(|elf| elf.parse::<u32>().expect("Number parsing map fail."));
    //for elf in elves { /* The iterated elf is u32 here */ }

    // expect is just unwrap without the message, only use when failure should explode the program

    let elves_totalled = elves.map(|elf| {
        elf.lines()
            .map(|line| line.parse::<u32>().unwrap())
            .sum::<u32>()
    });

    let mut elves_totalled: Vec<u32> = elves_totalled.collect();
    elves_totalled.sort_by(|a, b| b.cmp(a));

    let ans_a = *elves_totalled.first().unwrap();
    let ans_b = elves_totalled.iter().take(3).sum::<u32>();

    println!("Answer A: {ans_a}, Answer B: {ans_b} (using the functional approach)");
}

/// The simple for loop approach, the types a simpler here. I don't like this though.
fn for_loops(data: &String) {
    let lines: std::str::Lines = data.lines();

    let mut elf_total = 0;
    let mut totals: Vec<u32> = Vec::new();

    for line in lines {
        if line.trim().len() == 0 {
            totals.push(elf_total);
            elf_total = 0;
        } else {
            let num: u32 = line.parse().expect("Failed to parse a number?");
            elf_total += num;
        }
    }

    totals.push(elf_total);

    totals.sort_by(|a, b| b.cmp(a));

    let ans_a: u32 = *(totals.first().expect("no data?"));
    let ans_b: u32 = totals.iter().take(3).sum::<u32>();

    println!("Answer A: {ans_a}, Answer B: {ans_b} (using the for-loop approach)");
}
