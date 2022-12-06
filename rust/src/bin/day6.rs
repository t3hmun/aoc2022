use std::collections::{HashSet, VecDeque};

fn main() {
    let data = std::fs::read_to_string("../day-06-data.txt").unwrap();

    let part_one = answer_part_one(&data);
    let part_two = answer_part_two(&data);
    println!("part one: {part_one}, part two: {part_two}");
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
