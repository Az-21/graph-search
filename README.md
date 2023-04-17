# Graph Search

<a href="https://github.com/Az-21/graph-search/blob/main/LICENSE" alt="GPL 3.0">
<img src="https://img.shields.io/github/license/Az-21/graph-search?style=for-the-badge" /></a>
<a href="" alt="C#11">
<img src="https://img.shields.io/badge/Built%20With-C%20Sharp-%23630094?style=for-the-badge&logo=c-sharp" /></a>
<a href="" alt=".NET7">
<img src="https://img.shields.io/badge/Built%20On-.NET7-%234E2ACD?style=for-the-badge&logo=dotnet" /></a>

## Install

1. Download the `.7z` file from the [releases](https://github.com/Az-21/graph-search/releases/latest).
2. Install the latest '.NET Runtime 7' based on your system architecture from [Microsoft DotNet 7 Download](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) page.
	- If you are unsure about the system architecture, it is most likely `Windows x64`.

## Usage

1. Extract the `.7z` file downloaded earlier.
2. Run `Search.exe` to execute the program.

**NOTE**: Do **not** extract this application in a folder which requires admin privileges (eg: `Program Files`).

## Customize Graph and Search Parameters

This logic is programmed considering `int` based nodes. Internally, `A` is mapped to `0`, `B` is mapped to `1`, and so on. While this approach gives flexibility to add up to 2.1 billion nodes, it is also limiting in the fact that node graph has to be ordered. Program will extrapolate the values in first row as node `A | 0`, values in second row as node `B | 1`, and so on.

### Adjacency matrix

---

Create a new `.csv` file in `Graphs` folder. In this adjacency matrix. Each row represents the origin node and each non-primary diagonal column represents the travel cost. The primary diagonal encodes the heuristic cost h(n). Due to these constraints, this `.csv` file must have equal number of rows and columns. Value of `0` in a non-primary diagonal element implies that these nodes are not connected.

Taking a simple example to illustrate how adjacency matrix is parsed, consider the following matrix:

```csv
7, 1, 3, 4
1, 4, 2, 4
0, 6, 7, 8
5, 4, 2, 0
```

This matrix is parsed as:

```csv
h(A)  ,    A -> B,   A -> C,   A -> D
B -> A,    h(B)  ,   B -> C,   B -> D
C -> A,    C -> B,   h(C)  ,   C -> D
D -> A,    D -> B,   D -> C,   h(D)
```

The program comes with a sample adjacency matrix. It is recommended to copy-paste `matrix01.csv` in `Graphs` folder to create a new adjacency matrix `matrix02.csv`.

**NOTE** All `.csv` files will be treated as an adjacency matrix.

### Adjacency list

---

Create a new `.json` file in `Graphs` folder. In this adjacency list. Each node has a comma separated list of connected nodes. The travel cost is assumed to be `1`.

Taking a simple example to illustrate how adjacency list is parsed, consider the following list:

```json
{
  "A": "B, C",
  "B": "",
  "C": "A, B, D",
  "D": "C",
  "h(n)": "9, 7, 3, 0"
}
```

**NOTE**: The node keys (value before `:`) must be in alphabetic order starting with `A`. Even if a node is a dead end (like `B` is in the given example), it must be present in the `.json`.

**NOTE**: The very last key is `h(n)`. Enter the heuristic values of **all** nodes present in the graph in alphabetic order. In given example, there are 4 nodes, so 4 heuristic values are given. `h(A) = 9`, `h(B) = 7`, `h(C) = 3`, and `h(D) = 4`.

This adjacency list is parsed as the following adjacency matrix:

```csv
9, 1, 1, 0
0, 7, 0, 0
1, 1, 3, 1
0, 0, 1, 0
```

The program comes with a sample adjacency list. It is recommended to copy-paste `list01.json` in `Graphs` folder to create a new adjacency list `list02.json`.

**NOTE**: All `.json` files will be treated as an adjacency list.

### Configure search parameters

---

The configuration options can be found in `config.json` in `Graphs` folder. It allows you to set the graph, start node, and goal node.

```jsonc
{
  // Graph to work on; file must be present in the `Graphs` folder
  "GraphName": "matrix01.csv",

  "StartNode": "A", // "A" is equivalent to "0"
  "GoalNode":  "G"  // "G" is equivalent to "6"
}
```

After making changes in `config.json`, rerun `Simplify.exe`.
