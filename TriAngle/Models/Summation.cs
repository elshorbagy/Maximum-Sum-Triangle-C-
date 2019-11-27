using System;
using System.Collections.Generic;
using System.Text;
using TriAngleUi.Common;

namespace TriAngleUi.Models
{
    internal class Summation : ISummation
    {
        public ResultPath Result { get; set; }

        private readonly List<List<int>> _triangle;

        public List<List<int>> NumberPathList { get; set; }

        public Summation(string file)
        {
            _triangle = new List<List<int>>();
            NumberPathList = new List<List<int>>();

            if (!GetData(file)) return;

            Result = new ResultPath();
            GetMaxValue(new ResultPath(_triangle[0][0]));
        }

        public string GetSummation()
        {
            var result = new StringBuilder();
            var summationPath = Result.Path;

            int i;

            result.Append(Result.Sum + "|");

            for (i = 0; i < (summationPath.Count - 1); i++)
                result.Append(_triangle[i][summationPath[i]] + " -> ");

            result.Length = result.Length - 3;

            return result.ToString();
        }

        private bool GetData(string file)
        {
            try
            {
                var lines = FileRead.Read(file);
                if (lines.Length == 0)
                {
                    throw new Exception("There is no data to read in the file");
                }

                var dataItems = 1;

                for (var i = 0; i < lines.Length; i++)
                {
                    var itemCount = 0;
                    var line = lines[i];
                    var numbers = line.Split((string[])null, StringSplitOptions.RemoveEmptyEntries);
                    //NumbersCheckUp.IsTheSameGrope()

                    foreach (var inputNumber in numbers)
                    {
                        if (int.TryParse(inputNumber, out var number))
                        {
                            if (i < _triangle.Count)
                            {
                                _triangle[i].Add(number);
                                NumberPathList[i].Add(int.MinValue);
                            }
                            else
                            {
                                if (i >= _triangle.Count)
                                {
                                    _triangle.Add(new List<int>());
                                    NumberPathList.Add(new List<int>());
                                }
                                _triangle[i].Add(number);
                                NumberPathList[i].Add(int.MinValue);
                            }
                            itemCount++;
                        }
                        else
                        {                            
                            throw new Exception("Could not read the numbers, please check the file");
                        }
                    }
                    // Check if the current row has an extra number than the previous one
                    // To validate the triangle
                    if (itemCount != dataItems)
                    {                        
                        throw new Exception("Triangle Data are not valid");                        
                    }
                    dataItems++;
                }
                return true;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        private void GetMaxValue(ResultPath currentPath)
        {
            while (true)
            {
                if (currentPath.Row >= _triangle.Count) return;
                if (AlreadyExist(currentPath)) return;
                SaveNodesPath(currentPath.Row, currentPath.GetUpperIndex(), currentPath.Sum);
                var leftNode = CreateNode(currentPath, currentPath.GetUpperIndex());
                var rightNode = CreateNode(currentPath, currentPath.GetUpperIndex() + 1);

                if (currentPath.Sum > Result.Sum) Result = currentPath;

                GetMaxValue(leftNode);
                currentPath = rightNode;
            }
        }

        private void SaveNodesPath(int row, int index, int sum) =>
            NumberPathList[row][index] = sum;

        private ResultPath CreateNode(ResultPath currentPath, int index)
        {
            if (currentPath == null) throw new ArgumentNullException(nameof(currentPath));
            var trianglePath = (ICloneable)currentPath;
            var newNode = (ResultPath)trianglePath.Clone();

            newNode.Row++;

            if (newNode.Row >= _triangle.Count) return newNode;

            newNode.Path.Add(index);

            newNode.Sum += _triangle[newNode.Row][index];

            return newNode;
        }

        private bool AlreadyExist(ResultPath trianglePath)
        {
            var currentPath = trianglePath.Path;
            var currentSum = 0;
            for (var i = 0; i < currentPath.Count; i++)
            {
                var savedSum = NumberPathList[i][currentPath[i]];
                currentSum += _triangle[i][currentPath[i]];
                if (savedSum > currentSum) return true;
            }
            return false;
        }
    }
}