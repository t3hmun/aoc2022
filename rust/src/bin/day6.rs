use std::{
    collections::{HashSet, VecDeque},
    time::Instant,
};

fn main() {
    let data = std::fs::read_to_string("../day-06-data.txt").unwrap();

    let orig = Instant::now();
    for _ in 0..100 {
        answer_part_one(&data);
        answer_part_two(&data);
    }
    let orig_time = orig.elapsed();
    println!("orig {:?}", orig_time);

    let part_one = answer_part_one(&data);
    let one_good = part_one == 1356;
    let part_two = answer_part_two(&data);
    let two_good = part_two == 2564;
    println!(
        "part one: {} {}, part two: {} {}",
        part_one, one_good, part_two, two_good
    );
}

fn answer_part_one(data: &String) -> usize {
    // I thought this would be easy with the window function but that only works on slices, I think that means I'd have to collect the whole string.
    // This solution tries to minimise iteration.
    let mut queue: VecDeque<char> = VecDeque::new();
    let mut exists = HashSet::new();
    let mut idx: usize = 0;
    for ch in data.chars() {
        idx += 1;
        queue.push_back(ch);
        if queue.len() == 4 {
            exists.clear();
            let no_dupes = queue.iter().all(|c| exists.insert(*c));
            if no_dupes {
                break;
            }
            queue.pop_front();
        }
    }

    idx
}

fn answer_part_two(data: &String) -> usize {
    let mut idx = answer_part_one(data);
    let mut queue: VecDeque<char> = VecDeque::new();
    let mut exists = HashSet::new();
    for ch in data.chars().skip(idx) {
        idx += 1;
        queue.push_back(ch);
        if queue.len() == 14 {
            exists.clear();
            let no_dupes = queue.iter().all(|c| exists.insert(*c));
            if no_dupes {
                break;
            }
            queue.pop_front();
        }
    }

    idx
}
