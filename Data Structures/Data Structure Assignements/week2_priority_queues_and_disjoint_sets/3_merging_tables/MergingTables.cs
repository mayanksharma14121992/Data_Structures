using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergingTablesImplementation
{
	public delegate void InputsForProblem();
	public class MergingTables
    {
        public int DataRows { get; set; }
        public List<int> SymbolicLinkPath { get; set; }

        public long MaximumRecords;

        public MergingTables()
        {

        }
        public MergingTables(int rows)
        {
            DataRows = rows;
            SymbolicLinkPath = null;
        }

        public List<MergingTables> GetTablesList(int[] rows)
        {
            List<MergingTables> tables = new List<MergingTables>();
            MaximumRecords = rows[0];
            for (int i = 0; i < rows.Length; i++)
            {
                var localMaximum = rows[i];
                var table = new MergingTables(rows[i]);
                tables.Add(table);
                if (localMaximum > MaximumRecords)
                    MaximumRecords = localMaximum;
            }
            return tables;
        }

        public void PerformMergingOperation(int[] query, List<MergingTables> tablesList, out long maximum)
        {
            maximum = MaximumRecords;
            int left = query[0] - 1;
            int right = query[1] - 1;
            var sourceTable = tablesList[left];
            var endTable = tablesList[right];
            var sourceOriginalIndex = GetIndexOfOriginalList(tablesList, sourceTable, left);
            var endOriginalIndex = GetIndexOfOriginalList(tablesList, endTable, right);
            if (sourceOriginalIndex != endOriginalIndex)
            {
                if (left != sourceOriginalIndex)
                {
                    sourceTable = tablesList[sourceOriginalIndex];
                    //if (tablesList[left].SymbolicLinkPath == null)
                    //    tablesList[left].SymbolicLinkPath = new List<int>();
                    //tablesList[left].SymbolicLinkPath.Add(sourceOriginalIndex);
                }
                if (right != endOriginalIndex)
                {
                    endTable = tablesList[endOriginalIndex];
                    if (tablesList[right].SymbolicLinkPath == null)
                        tablesList[right].SymbolicLinkPath = new List<int>();
                    tablesList[right].SymbolicLinkPath.Add(sourceOriginalIndex);
                }
                var totalRows = sourceTable.DataRows + endTable.DataRows;
                if (totalRows > MaximumRecords)
                    MaximumRecords = totalRows;
                sourceTable.DataRows = totalRows;
                endTable.DataRows = 0;
                if (endTable.SymbolicLinkPath == null)
                    endTable.SymbolicLinkPath = new List<int>();
                endTable.SymbolicLinkPath.Add(sourceOriginalIndex);
                maximum = MaximumRecords;
            }
           
        }

        private int GetIndexOfOriginalList(List<MergingTables> tablesList, MergingTables table, int index)
        {
            int originalIndex = index;
            if(table.SymbolicLinkPath != null && table.SymbolicLinkPath.Any())
            {
                var pathLength = table.SymbolicLinkPath.Count;
                originalIndex = table.SymbolicLinkPath[pathLength - 1];
            }
            return originalIndex;
        }
    }
	
    class Program
    {
        static void Main(string[] args)
        {
            InputsForProblem input = InputsFormMergingTables;
            input.Invoke(); 
        }
		
		private static void InputsFormMergingTables()
        {
            var input = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            var tables = input[0];
            var mergeQueries = input[1];
            var rows = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            //ProcessMergingTableBruteForce(rows, mergeQueries);
            ProcessMergingTableOptimized(rows, tables, mergeQueries);
        }
		
		 private static void ProcessMergingTableBruteForce(int[] rows, int mergeQueries)
        {
            var mergingTables = new MergingTables();
            var getTablesList = mergingTables.GetTablesList(rows);
            long max;
            for (int i = 0; i < mergeQueries; i++)
            {
                var query = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                mergingTables.PerformMergingOperation(query, getTablesList, out max);
                Console.WriteLine(max);
            }
        }

        private static void ProcessMergingTableOptimized(int[] rows, int tables, int mergeQueries)
        {
            int[] rank = new int[tables];
            int[] parent = new int[tables];
            for (int i = 0; i < tables; i++)
            {
                rank[i] = 1;
                parent[i] = i;
            }
            long max = rows.Max();
            for (int i = 0; i < mergeQueries; i++)
            {
                var query = Console.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                MergeTables(rows, parent, rank, query[0] - 1, query[1] - 1, ref max);
            }
        }

        private static int GetParent(int table, int[] parent)
        {
            if (table != parent[table])
                parent[table] = GetParent(parent[table],parent);
            return parent[table];
        }

        private static void MergeTables(int[] rows, int[] parent, int[] rank, int destination, int source, ref long maximum)
        {
            long localMax = maximum;
            int originalDestination = GetParent(destination, parent);
            int originalSource = GetParent(source, parent);

            if(originalDestination == originalSource)
            {
                Console.WriteLine(localMax);
                return;
            }

            if(rank[originalDestination] > rank[originalSource])
            {
                parent[originalSource] = originalDestination;
                rows[originalDestination] += rows[originalSource];
                rows[originalSource] = 0;
                maximum = Math.Max(localMax, rows[originalDestination]);
            }
            else
            {
                parent[originalDestination] = originalSource;
                rows[originalSource] += rows[originalDestination];
                rows[originalDestination] = 0;
                maximum = Math.Max(localMax, rows[originalSource]);
                if (rank[originalSource] == rank[originalDestination])
                    rank[originalSource] += 1;
            }
            Console.WriteLine(maximum);
        }
        
    }
}
