using System;
using System.Collections.Generic;

namespace TriAngleUi.Models
{
    internal class ResultPath : ICloneable
    {
        public int Sum { get; set; }

        public int Row { get; set; }

        public List<int> Path { get; set; }

        public ResultPath()
        {
            Sum = 0;
            Row = 0;
            Path = new List<int>();
        }

        public ResultPath(int start)
        {
            Sum = start;
            Row = 0;
            Path = new List<int> { 0 };
        }

        object ICloneable.Clone()
        {
            var clone = new ResultPath { Row = Row, Sum = Sum };
            var pathClone = new List<int>();

            foreach (var index in Path)
                pathClone.Add(index);

            clone.Path = pathClone;
            return clone;
        }

        public int GetUpperIndex() =>
            Path.Count > 0 ? Path[Path.Count - 1] : -1;
    }
}