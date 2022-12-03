fn main() {
    let data = std::fs::read_to_string("../day-02-data.txt").unwrap();
    let part_one: i32 = data
        .lines()
        .map(|line| {
            let score = match line {
                "A Y" | "B Z" | "C X" => 6,
                "A Z" | "B X" | "C Y" => 0,
                "A X" | "B Y" | "C Z" => 3,
                _ => panic!("Invalid input"),
            };
            // Rust won't index a UTF-8 string unless you as_bytes it.
            // Reading UTF-8 code points is an forwards iterative process, hence nth.
            let you = line.chars().nth(2).unwrap();
            let points = match you {
                'X' => 1,
                'Y' => 2,
                'Z' => 3,
                _ => panic!("Invalid input"),
            };
            score + points
        })
        .sum();

    let part_two: i32 = data
        .lines()
        .map(|line| {
            let score = match line {
                "A Y" | "B X" | "C Z" => 1,
                "A Z" | "B Y" | "C X" => 2,
                "A X" | "B Z" | "C Y" => 3,
                _ => panic!("Invalid input"),
            };
            // Rust won't index a UTF-8 string unless you as_bytes it.
            // Reading UTF-8 code points is an forwards iterative process, hence nth.
            let you = line.chars().nth(2).unwrap();
            let points = match you {
                'X' => 0,
                'Y' => 3,
                'Z' => 6,
                _ => panic!("Invalid input"),
            };
            score + points
        })
        .sum();
    println!("Part One: {part_one}, Part Two: {part_two}");
}
