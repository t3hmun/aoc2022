fn main() {
    let data = std::fs::read_to_string("../day-03-test.txt").unwrap();
    let part_one = data
        .lines()
        .map(|line| {
            let bytes = line.as_bytes();

            println!("{:?}", bytes);

            bytes
        })
        .collect::<Vec<&[u8]>>();
}
