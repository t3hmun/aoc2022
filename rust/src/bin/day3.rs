fn main() {
    let data = std::fs::read_to_string("../day-03-data.txt").unwrap();
    let part_one: u32 = data
        .lines()
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
        .sum();

    println!("part one: {part_one}");
}
