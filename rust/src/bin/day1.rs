fn main() {
    let windows_data: &str = include_str!("./day1data.txt");
    let data = windows_data.replace("\r", "");
    let elves = data.split("\n\n");

    let mut parsed_elves: Vec<u32> = elves
        .map(|line| line.split("\n").flat_map(|s| s.parse::<u32>()).sum())
        .collect();
    parsed_elves.sort_by(|a, b| b.cmp(a));
    let ans_one = parsed_elves.iter().max().expect("no data?");
    //let ans_two = parsed_elves.iter().take(3).sum();

    println!("One: {ans_one}");
    //println!("Two: {ans_two}");
}
