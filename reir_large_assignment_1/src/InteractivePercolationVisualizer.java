/******************************************************************************
 *  Compilation:  javac InteractivePercolationVisualizer.java
 *  Execution:    java InteractivePercolationVisualizer n
 *  Dependencies: PercolationVisualizer.java Percolation.java
 *
 *  This program takes the grid size N as a command-line argument.
 *  Then, the user repeatedly clicks sites to open with the mouse.
 *  After each site is opened, it draws full sites in light blue,
 *  open sites (that aren't full) in white, and blocked sites in black.
 *
 ******************************************************************************/

import edu.princeton.cs.algs4.StdDraw;
import edu.princeton.cs.algs4.StdOut;


public class InteractivePercolationVisualizer {
    private static final int DELAY = 20;

    public static void main(String[] args) {
        // n-by-n percolation system (read from command-line, default = 10)
        int n = 0;
        if (args.length == 1) n = Integer.parseInt(args[0]);

        // turn on animation mode
        StdDraw.enableDoubleBuffering();

        // repeatedly open site specified my mouse click and draw resulting system
        StdOut.println(n);

        Percolation percolation = new Percolation(n);
        PercolationVisualizer.draw(percolation, n);
        StdDraw.show();

        while (true) {

            // detected mouse click
            if (StdDraw.isMousePressed()) {

                // screen coordinates
                double x = StdDraw.mouseX();
                double y = StdDraw.mouseY();

                // convert to (row, col)
                int row = (int) (n - Math.floor(y) - 1);
                int col = (int) (Math.floor(x));

                // open site (i, col) provided it's in bounds
                if (row >= 0 && row < n && col >= 0 && col < n) {
                    if (!percolation.isOpen(row, col)) { 
                        StdOut.println(row + " " + col);
                    }
                    percolation.open(row, col);
                }

                // draw n-by-n percolation system
                PercolationVisualizer.draw(percolation, n);
            }
            StdDraw.show();
            StdDraw.pause(DELAY);
        }
    }
}
