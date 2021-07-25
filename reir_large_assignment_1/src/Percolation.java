import edu.princeton.cs.algs4.*;


public class Percolation
{
    private QuickFindUF UF;
    private WeightedQuickUnionUF UFFace;
    private int open_counter = 0;
    private final int TOP;
    private final int BOTTOM;
    private final int N;
    private final int OUTER_BOUND;
    private final int INNER_BOUND;
    public boolean[][] grid;


    private int get_index(int row, int col){
        return row * N + col;
    }

    public Percolation(int n) // create N-by-N grid, with all sites initially blocked
    {
        if (n <= 0) {
            throw new IllegalArgumentException();
        }
        grid = new boolean[n][n];
        TOP = n*n;
        this.N = n;
        BOTTOM = n*n+1;
        UF = new QuickFindUF(n*n+2);
        UFFace = new WeightedQuickUnionUF(n*n+1);
        OUTER_BOUND = n-1;
        INNER_BOUND = 0;


        for(int i = 0; i<n; i++){
            UFFace.union(i, TOP);
            UF.union(n*n-i-1,BOTTOM);
        }
    }



    private boolean bound_check(int row, int col){
        return row < INNER_BOUND || row > OUTER_BOUND || col < INNER_BOUND || col > OUTER_BOUND;
    }
    public void open(int row, int col)// open the site (row, col) if it is not open already
    {
        int target = get_index(row, col);
        if (bound_check(row,col)){
            throw new IndexOutOfBoundsException();
        }

        if(!grid[row][col]){
            grid[row][col] = true;
            open_counter++;
            if(row == 0){
                UF.union(target, TOP);
            }

        }

        if (!bound_check(row+1, col) && grid[row+1][col]){
            UF.union(target, get_index(row+1, col));
            UFFace.union(target, get_index(row+1, col));
        }
        if (!bound_check(row-1, col) && grid[row-1][col]){
            UF.union(target, get_index(row-1, col));
            UFFace.union(target, get_index(row-1, col));
        }
        if (!bound_check(row, col+1) && grid[row][col+1]){
            UF.union(target, get_index(row, col+1));
            UFFace.union(target, get_index(row, col+1));
        }
        if (!bound_check(row, col-1) && grid[row][col-1]){
            UF.union(target, get_index(row, col-1));
            UFFace.union(target, get_index(row, col-1));
        }
    }

    public boolean isOpen(int row, int col)// is the site (row, col) open?
    {
        if(!bound_check(row, col)){
            return grid[row][col];
        }
        throw new IndexOutOfBoundsException();

    }

    public boolean isFull(int row, int col)// is the node (row, col) full?
    {
        if(!bound_check(row, col)){
            return UFFace.find(get_index(row, col)) == UFFace.find(TOP) && grid[row][col];
        }
        throw new IndexOutOfBoundsException();
    }

    public int numberOfOpenSites()  // number of open sites
    {
        return open_counter;
    }

    public boolean percolates() // does the system percolate?
    {
        return UF.find(TOP) == UF.find(BOTTOM);
    }

    public static void main(String[] args)   // unit testing (required)
    {
        // tests

        int N = 4;
        StdOut.println("N = 4");
        Percolation perc = new Percolation(N);

        StdOut.println("Initially node 0,0 is closed so calling perc.isopen will return false");
        StdOut.println(perc.isOpen(0, 0));
        StdOut.println("Opening 0,0 now and then calling perc.isopen again, which will return true");
        perc.open(0,0);
        StdOut.println(perc.isOpen(0, 0));
        StdOut.println(perc.percolates());
        StdOut.println("Printing the return of perc.percolates before and after running a loop opening random nodes");
        StdOut.println(perc.percolates());
        while (!perc.percolates()){
            int row = StdRandom.uniform(N);
            int col = StdRandom.uniform(N);
            perc.open(row, col);
        }
        StdOut.println(perc.percolates());
        StdOut.println("The number of nodes that were opened: " + perc.numberOfOpenSites());
        StdOut.println("============================================");
        StdOut.println("N = 4");
        perc = new Percolation(N);
        StdOut.println("Initally it does not percolate, percolation = " + perc.percolates());
        StdOut.println("We open bottom two (3,0) and (3,1)");
        perc.open(3, 0);
        perc.open(3, 1);
        StdOut.println("it does not percolate, percolation = " + perc.percolates());
        StdOut.println("We open top two (0,0) and (0,1)");
        perc.open(0, 0);
        perc.open(0, 1);
        StdOut.println("it does not percolate, percolation = " + perc.percolates());
        StdOut.println("Now we open (1,1) and (2,1)");
        perc.open(1, 1);
        perc.open(2, 1);
        StdOut.println("It percolates, percolation = " + perc.percolates());
    }

}