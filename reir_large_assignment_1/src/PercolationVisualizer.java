/******************************************************************************
 *  Compilation:  javac PercolationVisualizer.java
 *  Execution:    java PercolationVisualizer input.txt
 *  Dependencies: Percolation.java
 *
 *  This program takes the name of a file as a command-line argument.
 *  From that file, it
 *
 *    - Reads the grid size n of the percolation system.
 *    - Creates an n-by-n grid of sites (initially all blocked)
 *    - Reads in a sequence of sites (row, col) to open.
 *
 *  After each site is opened, it draws full sites in light blue,
 *  open sites (that aren't full) in white, and blocked sites in black,
 *  with with site (0, 0) in the upper left-hand corner.
 *
 ******************************************************************************/

import java.awt.Font;

import edu.princeton.cs.algs4.In;
import edu.princeton.cs.algs4.StdDraw;
import edu.princeton.cs.algs4.StdRandom;

public class PercolationVisualizer {

    // delay in milliseconds (controls animation speed)
    // 100 by default
    private static final int DELAY = 1;

    // draw n-by-n percolation system
    public static void draw(Percolation percolation, int n) {
        StdDraw.clear();
        StdDraw.setPenColor(StdDraw.BLACK);
        StdDraw.setXscale(-0.05*n, 1.05*n);
        StdDraw.setYscale(-0.05*n, 1.05*n);   // leave a border to write text
        StdDraw.filledSquare(n/2.0, n/2.0, n/2.0);

        // draw n-by-n grid
        for (int row = 0; row < n; row++) {
            for (int col = 0; col < n; col++) {
                if (percolation.isFull(row, col)) {
                    StdDraw.setPenColor(StdDraw.BOOK_LIGHT_BLUE);
                }
                else if (percolation.isOpen(row, col)) {
                    StdDraw.setPenColor(StdDraw.WHITE);
                }
                else {
                    StdDraw.setPenColor(StdDraw.BLACK);
                }
                StdDraw.filledSquare(col + 0.5, n - row - 0.5, 0.45);
            }
        }

        // write status text
        StdDraw.setFont(new Font("SansSerif", Font.PLAIN, 12));
        StdDraw.setPenColor(StdDraw.BLACK);
        StdDraw.text(0.25*n, -0.025*n, percolation.numberOfOpenSites() + " open sites");
        if (percolation.percolates()) StdDraw.text(0.75*n, -0.025*n, "percolates");
        else                          StdDraw.text(0.75*n, -0.025*n, "does not percolate");

    }

    private static void simulateFromFile(String filename) {
        //In in = new In(filename);
        //int n = in.readInt();
        int n = 100;
        Percolation percolation = new Percolation(n);

        // turn on animation mode
        StdDraw.enableDoubleBuffering();

        // repeatedly read in sites to open and draw resulting system
        draw(percolation, n);
        StdDraw.show();
        StdDraw.pause(DELAY);


        //while (!in.isEmpty()) {
        while(percolation.percolates() == false){

            int row = StdRandom.uniform(n);
            int col = StdRandom.uniform(n);
            //int row = in.readInt();
            //int col = in.readInt();
            percolation.open(row, col);
            draw(percolation, n);
            StdDraw.show();
            StdDraw.pause(DELAY);
        }
        System.out.println("LOl");

    }

    public static void main(String[] args) {
        //String filename = args[0];
        simulateFromFile("lol");

    }
}
