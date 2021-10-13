using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerProblems.Solutions
{
    /// <summary>
    /// Problem #766 (https://projecteuler.net/problem=766)
    /// </summary>
    public class Problem766 : IProblem<Problem766.Arguments>
    {
        #region Nested Types

        public class Arguments : List<Item>
        {
            private Arguments(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public int Width { get; }

            public int Height { get; }

            public static Arguments CreateTestInstance()
            {
                return new Arguments(4, 3)
                {
                    new("green", (0, 0), (0, 1), (1, 0)),
                    new("red", (0, 2)),
                    new("red", (1, 1)),
                    new("red", (1, 2)),
                    new("red", (2, 0)),
                    new("red", (2, 1)),
                    new("red", (2, 2)),
                    new("red", (2, 3)),
                };
            }

            public static Arguments CreatePuzzleInstance()
            {
                return new Arguments(6, 5)
                {
                    new("red", (0, 1), (0, 2), (1, 1)),
                    new("green", (0, 3), (1, 2), (1, 3)),
                    new("red", (0, 4), (0, 5), (1, 4)),
                    new("yellow", (1, 5), (2, 5)),
                    new("purple", (2, 0)),
                    new("purple", (2, 1)),
                    new("blue", (2, 2), (2, 3), (3, 2), (3, 3)),
                    new("yellow", (2, 4), (3, 4)),
                    new("purple", (3, 0)),
                    new("purple", (3, 1)),
                    new("green", (3, 5), (4, 4), (4, 5)),
                    new("purple", (4, 0)),
                    new("purple", (4, 1)),
                    new("azure", (4, 2), (4, 3)),
                };
            }
        }

        public class Item
        {
            public Item(string familyName, params (int Row, int Col)[] squares)
            {
                FamilyName = familyName;
                Squares = squares;
            }

            public string FamilyName { get; }

            public (int Row, int Col)[] Squares { get; }

            public bool IsValid(int rows, int cols) => Squares.All(i => i.Row >= 0 && i.Row < rows && i.Col >= 0 && i.Col < cols);
        }

        private enum Move
        {
            Left,
            Up,
            Right,
            Down,
        };

        private class Search
        {
            private readonly List<int> _familyCounts = new();
            private readonly Dictionary<(int, Move), int> _moves = new();

            public Search(Arguments args)
            {
                Width = args.Width;
                Height = args.Height;

                Items = args.OrderBy(i => i.FamilyName).ToArray();
                int curCount = 0;

                for (int i = 0; i < Items.Length; i++)
                {
                    if (i == 0 || Items[i].FamilyName == Items[i - 1].FamilyName)
                    {
                        curCount++;
                    }
                    else
                    {
                        _familyCounts.Add(curCount);
                        curCount = 1;
                    }
                }
                
                _familyCounts.Add(curCount);

                for (int familyIndex = 0, itemIndex = 0; familyIndex < _familyCounts.Count; itemIndex += _familyCounts[familyIndex], familyIndex++ )
                {
                    var item = Items[itemIndex];

                    for (int rowSquareOffset = -Height; rowSquareOffset <= Height; rowSquareOffset++)
                    {
                        for (int colSquareOffset = -Width; colSquareOffset <= Width; colSquareOffset++)
                        {
                            foreach ((int rowMoveOffset, int colMoveOffset, Move move) in GetMoves())
                            {
                                var offsetItem = new Item(string.Empty,
                                    item.Squares.Select(s => (s.Row + rowSquareOffset, s.Col + colSquareOffset)).ToArray());

                                if (!offsetItem.IsValid(Height, Width))
                                    continue;
                                
                                var movedItem = new Item(string.Empty,
                                    offsetItem.Squares.Select(s => (s.Row + rowMoveOffset, s.Col + colMoveOffset)).ToArray());
                                
                                if (!movedItem.IsValid(Height, Width))
                                    continue;

                                int mask = offsetItem.Squares.Aggregate(0, (acc, curr) => acc | GetSquareMask(curr.Row, curr.Col));
                                int movedMask = movedItem.Squares.Aggregate(0, (acc, curr) => acc | GetSquareMask(curr.Row, curr.Col));

                                _moves[(mask, move)] = movedMask;
                            }
                        }
                    }
                }

                InitialState = new State(Items.Select(GetItemMask).ToArray(), _familyCounts);

                int GetItemMask(Item item) => item.Squares.Aggregate(0, (acc, curr) => acc | GetSquareMask(curr.Row, curr.Col));
            }

            private static IEnumerable<(int RowOffset, int ColOffset, Move Move)> GetMoves()
            {
                yield return (0, +1, Move.Left);
                yield return (-1, 0, Move.Up);
                yield return (0, -1, Move.Right);
                yield return (+1, 0, Move.Down);
            }

            private int SquareToIndex(int row, int col) => row * Width + col;

            private int GetSquareMask(int row, int col) => 1 << SquareToIndex(row, col);

            public IEnumerable<State> GetNeighbourStates(State state)
            {
                for (int i = 0; i < state.ItemStates.Length; i++)
                {
                    int curMask = state.ItemStates[i];

                    foreach (var move in Directions)
                    {
                        if (!_moves.TryGetValue((curMask, move), out int movedMask))
                            continue;

                        if ((state.Mask & ~curMask & movedMask) != 0)
                            continue;

                        int[] itemStates = CopyWith(state.ItemStates, i, movedMask);

                        yield return new State(itemStates, _familyCounts);
                    }
                }

                static T[] CopyWith<T>(T[] source, int index, T newItem)
                {
                    var array = new T[source.Length];

                    for (int i = 0; i < source.Length; i++)
                    {
                        array[i] = i == index ? newItem : source[i];
                    }

                    return array;
                }
            }

            private static readonly Move[] Directions = { Move.Left, Move.Up, Move.Right, Move.Down };

            private int Width { get; }

            private int Height { get; }

            private Item[] Items { get; }

            public State InitialState { get; }
        }

        private readonly struct State : IEquatable<State>
        {
            private readonly int[] _familyMasks;

            public State(int[] itemStates, IReadOnlyList<int> familyCounts)
            {
                ItemStates = new int[itemStates.Length];
                Array.Copy(itemStates, ItemStates, ItemStates.Length);

                _familyMasks = new int[familyCounts.Count];
                Mask = 0;
                
                for (int familyIndex = 0, start = 0; familyIndex < familyCounts.Count; start += familyCounts[familyIndex], familyIndex++)
                {
                    int familyMask = 0;

                    for (int itemIndex = 0; itemIndex < familyCounts[familyIndex]; itemIndex++)
                    {
                        familyMask |= ItemStates[start + itemIndex];
                    }

                    _familyMasks[familyIndex] = familyMask;
                    Mask |= familyMask;
                }
            }

            public int Mask { get; }

            public int[] ItemStates { get; }

            public bool Equals(State other) => _familyMasks.SequenceEqual(other._familyMasks);

            public override bool Equals(object obj) => obj is State other && Equals(other);

            public override int GetHashCode()
            {
                int hash = 17;

                foreach (int familyMask in _familyMasks)
                {
                    hash = hash * 23 + familyMask;
                }
                
                return hash;
            }
        }

        #endregion

        #region Methods

        public string Solve(Arguments args)
        {
            var search = new Search(args);
            var visited = new HashSet<State>();
            var open = new Stack<State>();

            open.Push(search.InitialState);

            while (open.Any())
            {
                var state = open.Pop();
                visited.Add(state);

                foreach (var nextState in search.GetNeighbourStates(state))
                {
                    if(visited.Contains(nextState))
                        continue;
                    
                    open.Push(nextState);
                }
            }

            return visited.Count.ToString();
        }

        #endregion
    }
}