public class SudokuSolver {
    private int[] rowRestrictedNumbers;
    private int[] columnRestrictedNumbers;
    private int[] squareRestrictedNumbers;

    //[] - row, [] - column
    public void solveSudoku(char[][] board) {
        if(board == null)
            return;

        rowRestrictedNumbers = new int[9];
        columnRestrictedNumbers = new int[9];
        squareRestrictedNumbers = new int[9];
        fillInitialRestrictions(board);
        sudokuSolverHelper(board);
    }

    private boolean sudokuSolverHelper(char[][] board) {
        int availableNumbers, ri = 0, ci = 0, count = 0;
        int minAvailableNumbers = Integer.MAX_VALUE;
        for(int row = 0 ; row < 9 ; row++) {
            for (int column = 0; column < 9; column++) {
                if(board[row][column] == '.') {
                    availableNumbers = getAvailableNumbersCountForPosition(row, column);
                    if(minAvailableNumbers > availableNumbers) {
                        minAvailableNumbers = availableNumbers;
                        ri = row;
                        ci = column;
                    }
                    count++;
                }
            }
        }

        if(count == 0)
            return false;

        int arr = getAvailableNumbersForPosition(ri, ci);
        for(int i = 0; i < 9; i++) {
            int value = 1 << i;
            if ((arr & value) != 0)
                continue;

            board[ri][ci] = (char) (i + '1');
            if(count == 1)
                return true;

            setNumberToRestrictedArrays(ri, ci, value);
            if(sudokuSolverHelper(board))
                return true;

            removeNumberFromRestrictedArrays(ri, ci, value);
            board[ri][ci] = '.';
        }
        return false;
    }

    private void fillInitialRestrictions(char[][] board){
        for(int row = 0 ; row < 9 ; row++) {
            for (int column = 0; column < 9; column++) {
                if(board[row][column] == '.')
                    continue;
                int value = (1 << (board[row][column]-'1'));
                setNumberToRestrictedArrays(row, column, value);
            }
        }
    }

    private void setNumberToRestrictedArrays(int row, int column, int value) {
        setNumberToRestricted(rowRestrictedNumbers, row, value);
        setNumberToRestricted(columnRestrictedNumbers, column, value);
        setNumberToRestricted(squareRestrictedNumbers, getSquareFromPosition(row, column), value);
    }

    private void removeNumberFromRestrictedArrays(int row, int column, int value) {
        removeNumberFromRestricted(rowRestrictedNumbers, row, value);
        removeNumberFromRestricted(columnRestrictedNumbers, column, value);
        removeNumberFromRestricted(squareRestrictedNumbers, getSquareFromPosition(row, column), value);
    }

    //if 1 then restricted;
    private void setNumberToRestricted(int[] arr, int position, int value) {
        arr[position] |= value;
    }

    private void removeNumberFromRestricted(int[] arr, int position, int value) {
        arr[position] &= ~value;
    }

    private int getAvailableNumbersForPosition(int row, int column) {
        int r = rowRestrictedNumbers[row];
        int c = columnRestrictedNumbers[column];
        int s = squareRestrictedNumbers[getSquareFromPosition(row, column)];
        return r | c | s;
    }

    private int getAvailableNumbersCountForPosition(int row, int column) {
        int x = getAvailableNumbersForPosition(row, column);
        int count = 0;
        for(int i = 0; i < 9; i++) {
            count += (~(x >> i) & 1);
        }
        return count;
    }

    private int getSquareFromPosition(int row, int column) {
        return row / 3 * 3 + column / 3;
    }
}