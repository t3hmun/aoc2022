fn main() {
    let data = include_str!("./data.txt")
        .replace("\r", "")
        .split("\n\n")
        .map(|g| g.split("\n").map(|e| e.parse::<i64>().expect("parse fail")));

    for x in data {
        println!("{x}");
    }
}
