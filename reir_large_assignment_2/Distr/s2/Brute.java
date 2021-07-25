package s2;

import edu.princeton.cs.algs4.In;
import edu.princeton.cs.algs4.Out;
import edu.princeton.cs.algs4.StdOut;

import java.lang.reflect.Array;
import java.util.Arrays;

public class Brute {
    static Point[] points;
    static Point[] lines;
    public String[] line_collection;

    public Brute (Point[] points){
        int length = points.length;
        line_collection = new String[length];
        int line_index = 0;
        for(int a = 0; a < length-3; a++){
            for(int b = a+1; b < length-2; b++){
                for(int c = b+1; c < length-1; c++){
                    for(int d = c+1; d < length; d++){

                        Point point1 = points[a];
                        Point point2 = points[b];
                        Point point3 = points[c];
                        Point point4 = points[d];
                        // Point 1 Slopes
                        double slope1to2 = point1.slopeTo(point2);
                        double slope1to3 = point1.slopeTo(point3);
                        double slope1to4 = point1.slopeTo(point4);
                        boolean check = slope1to2 == slope1to3 && slope1to3 == slope1to4;

                        if(check){
                            StdOut.println(point1 + " -> " + point2 + " -> " + point3 + " -> " + point4);
                            line_collection[line_index] = point1 + " -> " + point2 + " -> " + point3 + " -> " + point4;
                            line_index ++;
                        }
                    }
                }
            }
        }
    }

    public static void main(String[] args) {
        In in = new In(args[0]);
        int n = in.readInt();
        points = new Point[n];
        lines = new Point[n];
        for (int i = 0; i < n; i++) {
            int x = in.readInt(), y = in.readInt();
            points[i] = new Point(x, y);
       }
        Arrays.sort(points);
        Brute force = new Brute(points);
    }
}

