use std::{num::ParseIntError, str::FromStr};

#[derive(Debug)]
struct CrateMove {
    quantity: usize,
    source: usize,
    target: usize,
}

impl FromStr for CrateMove {
    type Err = ParseIntError;

    fn from_str(s: &str) -> Result<Self, Self::Err> {
        let parts: Vec<&str> = s.split(" ").collect();

        // The ParseIntError is propagated via the magical question mark.
        Ok(CrateMove {
            quantity: parts[1].parse()?,
            source: parts[3].parse()?,
            target: parts[5].parse()?,
        })
    }
}

fn main() {
    let data = std::fs::read_to_string("../day-05-data.txt").unwrap();

    let part_one = answer_part_one(&data);
    let part_two = answer_part_two(&data);
    println!("part one: {part_one}, part two: {part_two}");
}

fn answer_part_one(data: &String) -> String {
    let (mut stacks, moves) = parse_data(data);
    for mov in moves {
        let mut temp = Vec::new();
        {
            let source = &mut (stacks[mov.source - 1]);
            for _ in 0..mov.quantity {
                let item = source.pop().unwrap();
                temp.push(item);
            }
        }
        {
            let target = &mut stacks[mov.target - 1];
            for item in temp {
                target.push(item);
            }
        }
    }

    // flat_map is sort of flattening the Option, only returns the ones that were successful.
    let ans: String = stacks.iter().flat_map(|s| s.last()).collect();

    // let mut ans = String::new();
    // for stack in stacks {
    //     if stack.len() > 0 {
    //         let top = *stack.last().unwrap();
    //         ans.push(top);
    //     }
    // }

    ans
}

fn answer_part_two(_data: &String) -> &str {
    return "todo";
}

fn parse_data(data: &String) -> (Vec<Vec<char>>, Vec<CrateMove>) {
    // I designed this to iterate the lines of the string exactly once (except for the first line).

    // Needs to be mut to be used without consuming it.. that sort of makes sense to me, maybe.
    let mut iter = data.lines();
    let mut stacks = Vec::new();
    let mut moves = Vec::new();

    let first_line = data.lines().next().unwrap();
    let stack_count: usize = (first_line.len() + 1) / 4;

    for _ in 0..stack_count {
        let stack: Vec<char> = Vec::new();
        stacks.push(stack);
    }

    // Use the iterator without consuming it so it can be resumed.
    for line in iter.by_ref() {
        // Stop when reaching the labels for the stack picture.
        if line.starts_with(" 1") == true {
            break;
        }
        //println!("stack {}", line);

        // A single iteration of the chars would be nice here but pointless effort.
        let chars = line.chars().collect::<Vec<char>>();

        for i in 0..stack_count {
            let idx = (i * 4) + 1;
            // crate is a reserved word :/
            let item = chars[idx];
            if item != ' ' {
                stacks[i].insert(0, item);
            }
        }
    }

    iter.by_ref().next(); // Skip the blank line.

    for line in iter.by_ref() {
        let crate_move = CrateMove::from_str(line).expect("failed move parse.");
        moves.push(crate_move);
    }

    //println!("{:?}", stacks);
    //println!("{:?}", moves);
    (stacks, moves)
}
