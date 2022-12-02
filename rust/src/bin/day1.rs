/* This is the first Rust I've ever written so maximum qualification and annotations.
* Random notes:
* - Reading the first 5 chapters of the Rust Book was essential
* - The types for strings
    - Not magical like they are in C# and JS.
    - Not ASCII char array like in C
    - UTF-8 byte array, len might be different to number of letters
    - Different types for compiled literals (str) and runtime growable strings (Strings)
*/

fn main() {
    let data: String = std::fs::read_to_string("../day-01-data.txt").expect("Failed to read file.");
    for_loops(&data);
    map_reduce(&data);
}

/// This is my instinctive way to do it but understanding types returned by each function isn't obvious.
fn map_reduce(data: &String) {
    let ans_a = "todo";
    let ans_b = "todo";

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
