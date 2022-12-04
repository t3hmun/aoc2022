fn main() {
    let data = std::fs::read_to_string("../day-04-data.txt").unwrap();

    let part_one = answer_part_one(&data);
    let part_two = answer_part_two(&data);
    println!("part one: {part_one}, part two: {part_two}");
}

fn answer_part_one(data: &String) -> u32 {
    let pattern = regex::Regex::new(r"^(\d+)-(\d+),(\d+)-(\d+)").unwrap();
    let mut total: u32 = 0;
    for line in data.lines() {
        let caps = pattern.captures(line).expect("No match");
        let nums = caps
            .iter()
            .skip(1)
            .map(|c| c.unwrap().as_str().parse::<u32>().unwrap())
            .collect::<Vec<u32>>();

        let [l1, r1, l2, r2] = [nums[0], nums[1], nums[2], nums[3]];

        if (l1 <= l2 && r1 >= r2) || (l2 <= l1 && r2 >= r1) {
            total += 1;
        }
    }
    total
}

fn answer_part_two(data: &String) -> u32 {
    let pattern = regex::Regex::new(r"^(\d+)-(\d+),(\d+)-(\d+)").unwrap();
    let mut total: u32 = 0;
    for line in data.lines() {
        let caps = pattern.captures(line).expect("No match");
        let nums = caps
            .iter()
            .skip(1)
            .map(|c| c.unwrap().as_str().parse::<u32>().unwrap())
            .collect::<Vec<u32>>();

        let [l1, r1, l2, r2] = [nums[0], nums[1], nums[2], nums[3]];

        if (l1 >= l2 && l1 <= r2)
            || (l2 >= l1 && l2 <= r1)
            || (l2 >= l1 && l2 <= r1)
            || (l1 >= l2 && l1 <= r2)
        {
            total += 1;
        }
    }
    total
}
