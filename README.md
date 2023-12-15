# Intro

This repo contains random experiments and solutions for Advent of Code and LeetCode. 

# Personal Rules

## Personal rules: Advent Of Code
- No AI tools (GH CoPilot, ChatGPT)
- Each year the goal is to do each puzzle within 24 hours of it getting released, and to see how long you can survive
- No looking up solutions at all, even to learn from
- Focus is completing as fast as possible, not optimal solution
- No 3rd party libs (personally built libs and standard library is fine)
- No asking for help from other people; no discussing problems until after they're complete
- Googling is fine as long as not looking up solutions
- For each solution, the initial commit will reflect the exact code at time of submission. I may clean up the code at some point later with subsequent commits.

## Personal rules: LeetCode
- Max 3 attempts per problem. If failed all 3 attempts, leave and come back on different day
- Max 30 min per problem. If fail to solve, leave and come back on different day
- Look up a solution or use AI tools for help only to learn from, but if you do then don't attempt the problem until at least a month has gone by
- Focus is on optimal time complexity

# Progress

## Progress: Advent of Code

**★**  = Part completed within 24 hours of release  
*✓* = Part completed some time after  
*: Solution code missing in this repo

| Year | Day 1 | Day 2 | Day 3 | Day 4 | Day 5 | Day 6 | Day 7  | Day 8   | Day 9   | Day 10 | Day 11 | Day 12 | Day 13  | Day 14  | Day 15 | Day 16    | Day 17 | Day 18 | Day 19 | Day 20 | Day 21 | Day 22 | Day 23 | Day 24 | Day 25 |
|------|-------|-------|-------|------|-------|------|--------|---------|---------|-------|-----|------|---------|---------|--------|-----------|--------|--------|--------|--------|--------|--------|--------|--------|--------|
| 2023 | **★★** | **★★** | **★★** | **★★** | **★★** | **★★** | **★★** | **★★**  | **★★**  | **★** | **★★** | **✓** | **★**   | **★★**  | **★★** |           |        |        |        |        |        |        |        |        |        |
| 2022 | **★★** | **★★** | **★★** | **★★** | **★★** | **★★** | **★★** | **★★**  | **★★**  | **★★** | **★★** | **★★** | **★***✓* |         |        |           |        |        |        |        |        |        |        |        |        |
| 2021 | **★★*** | **★★*** | **★★*** | **★★** | **★★*** | **★★*** | **★★*** | **★★*** | **★★*** | **★★*** | **★★*** | **★★*** | **★★*** | **★★*** | **★★*** | **★***✓** |        |        |        |        |        |        |        |        |        |
| 2020 | **★★*** | **★★** | **★★*** | **★★*** | **★★*** | **★★*** | **★★*** | **★★*** | **★★*** |       |     |      |         |         |        |           |        |        |        |        |        |        |        |        |        |
| 2019 | **★★*** | **★★*** | **★★*** | **★★*** | **★★*** | *✓✓* |        |         |         |       |     |      |         |         |        |           |        |        |        |        |        |        |        |        |        |
| 2018 |       |       |       |      |       |      |        |         |         |       | *✓* |      |         |         |        |           |        |        |        |        |        |        |        |        |        |
| 2017 |       |       |       | *✓✓* |       |      |        |         |         |       |     | *✓✓* |         |         |        |           |        |        |        |        |        |        |        |        |        |
| 2016 |       |       |       |      |       | *✓✓* |        |         |         |       |     |      | *✓✓*    |         |        |           |        |        |        |        |        |        |        |        |        |
| 2015 | *✓✓** | *✓✓** | *✓✓** | *✓✓** | *✓✓** | *✓✓** |        | *✓✓*    |         |       |     |      |         |         |        |           |        |        |        |        |        |        |        |        |        |

## Progress: LeetCode

https://leetcode.com/ncn-ends/

# Variable names
`p` pointer (single)  
`p1-5` pointers (multiple)
`s` stack   
`q` queue  
`l` list  
`c` catch-all "current"  
`r` placeholder to be returned

### File Templates for C#

`ild`  
```
[InlineData($END$)]
```  
  
`theory`
```
    [Theory]
    public async Task $methodName$(){
        $END$
    }
```

`arr`
```
new[] {$END$}
```