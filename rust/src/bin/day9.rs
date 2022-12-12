use anyhow::Ok;
use std::{collections::HashSet, str::FromStr};
use vect::{vector::Vector, vector2::Vector2};

fn main() {
    println!("day 9");

    let data = std::fs::read_to_string("../day-09-data.txt").unwrap();
    let commands = data.lines().map(|line| Command::from_str(line).unwrap());
    let mut head = Vector2::zero();
    let mut tail = Vector2::zero();
    let mut visits_p1 = HashSet::new();
    let mut visits_p2 = HashSet::new();
    let mut tails: Vec<Vector2> = Vec::new();

    for _ in 0..=9 {
        tails.push(Vector::zero())
    }

    for command in commands {
        for _ in 0..command.repeat {
            head = head + command.movement;
            tail = tail_move(&head, tail);
            visits_p1.insert((tail.x as i64, tail.y as i64));

            tails[0] = tails[0] + command.movement;
            for i in 1..=9 {
                tails[i] = tail_move(&tails[i - 1], tails[i]);
            }

            visits_p2.insert((tails[9].x as i64, tails[9].y as i64));
        }
    }
    println!("part one: {:?}", visits_p1.len());
    println!("part two: {:?}", visits_p2.len());
}

#[derive(Debug)]
struct Command {
    //direction: char,
    movement: Vector2,
    repeat: usize,
}

impl FromStr for Command {
    type Err = anyhow::Error;

    fn from_str(s: &str) -> Result<Self, Self::Err> {
        let mut parts = s.split(" ");
        // unwrap every other statement is good rust.
        let dir = parts.next().unwrap().chars().next().unwrap();
        let amount: usize = parts.next().unwrap().parse().unwrap();
        let v = match dir {
            'U' => Some(Vector2::up()),
            'D' => Some(Vector2::down()),
            'L' => Some(Vector2::left()),
            'R' => Some(Vector2::right()),
            _ => None,
        }
        .unwrap();

        Ok(Command {
            //direction: dir,
            movement: v,
            repeat: amount,
        })
    }
}

// My first ever trait.
trait Away {
    fn ceil_away_from_zero(&self) -> Self;
}

impl Away for f64 {
    fn ceil_away_from_zero(&self) -> Self {
        if *self > 0.0 {
            self.ceil()
        } else if *self < 0.0 {
            self.floor()
        } else {
            *self
        }
    }
}

fn tail_move(h: &Vector2, t: Vector2) -> Vector2 {
    let diff = *h - t;

    // This feels like a dirty hack instead of clever maths.
    if diff.magnitude() > 1.5 {
        let simple = Vector2 {
            x: (diff.x / 2.0).ceil_away_from_zero(),
            y: (diff.y / 2.0).ceil_away_from_zero(),
        };
        t + simple
    } else {
        t
    }
}
