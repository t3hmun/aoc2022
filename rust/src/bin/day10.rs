use std::fs;

fn main() {
    println!("hi");
    let data = fs::read_to_string("../day-10-test.txt").unwrap();
    let lines = data.lines();

    let mut cycle: usize = 0;

    for line in lines {
        cycle += 1;
        AddSignal();
        WritePixel();

        if line == "noop" {
            continue;
        }

        let add_num = line.chars().skip(5).collect::<String>();
        println!("num:{}", add_num);
    }
}

fn AddSignal() {}

fn WritePixel() {}
