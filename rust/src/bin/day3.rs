fn main() {
    let data = std::fs::read_to_string("../day-03-data.txt").unwrap();

    let part_one = answer_part_one(&data);
    let part_two = answer_part_two(&data);
    println!("part one: {part_one}, part two: {part_two}");
}

fn answer_part_two(data: &String) -> u32 {
    let lines = data.lines().collect::<Vec<&str>>(); // .array_chunks(); chunks is experimental :(

    let num_of_lines = lines.len();

    println!("num {:?}", num_of_lines);
    let mut total: u32 = 0;

    // array_chunks would have been nicer but that's experimental.
    for i in (0..num_of_lines).step_by(3) {
        let group = &lines[i..i + 3];
        let first = group[0];

        // My first Option.
        let mut in_all = None;
        let chars = first.chars();
        for char in chars {
            let is_in_all = group
                .iter()
                .skip(1)
                .map(|line| line.chars().any(|c| c == char))
                .all(|b| b == true);

            if is_in_all == true {
                in_all = Some(char);
                break;
            }
        }

        let b = in_all.unwrap() as u8;
        let pri = match b {
            65..=90 => b as u32 - 38,
            97..=122 => b as u32 - 96,
            _ => panic!("Unexpected byte val"),
        };

        total += pri;
    }

    total
}

fn answer_part_one(data: &String) -> u32 {
    data.lines()
        .map(|line| {
            let bytes = line.as_bytes();
            let side_len = bytes.len() / 2;
            let left = &bytes[0..side_len];
            let right = &bytes[side_len..];
            let mut both = Vec::<u8>::new();

            for bl in left {
                for br in right {
                    if bl == br {
                        both.push(*bl);
                    }
                }
            }
            both.sort();
            both.dedup();
            // A=65 Z=90 a=97 z=122
            let priorities = both
                .iter()
                .map(|b| match b {
                    65..=90 => *b as u32 - 38,
                    97..=122 => *b as u32 - 96,
                    _ => panic!("Unexpected byte val"),
                })
                .collect::<Vec<u32>>();

            let total_pri: u32 = priorities.iter().sum();

            //println!("\nBoth: {:?}", both);
            //println!("PRi: {:?}", priorities);
            //println!("total: {:?}", total_pri);

            total_pri
        })
        .sum()
}
