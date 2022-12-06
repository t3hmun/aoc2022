fn main() {
    let data = std::fs::read_to_string("../day-06-test1.txt").unwrap();

    let part_one = answer_part_one(&data);
    let part_two = answer_part_two(&data);
    println!("part one: {part_one}, part two: {part_two}");
}

fn answer_part_one(data: &String) -> String {
    String::from("todo")
}

fn answer_part_two(data: &String) -> String {
    String::from("todo")
}
