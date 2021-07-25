package s2;

import edu.princeton.cs.algs4.In;
import edu.princeton.cs.algs4.StdOut;

import java.util.Arrays;

    public class Fast {
    static Point[] points;
    static Point[] lines;

    public Fast(Point[] points){
        int length = points.length;
        for(int a = 0; a < length - 1; a++){
            Point[] points_temp = createCompareArray(a, points, length);
            Arrays.sort(points_temp, points[a].SLOPE_ORDER);
            findLines(points[a], points_temp);
        }
    }

    private Point[] createCompareArray(int a, Point[] points, int length){
        Point[] points_temp;

        int slope_index;
        points_temp = new Point[length-a-1];

        slope_index = 0;

        for(int b = a + 1; b < length ; b++){

            points_temp[slope_index] = points[b];
            slope_index++;
        }
        return points_temp;
    }

    private void findLines(Point main_point, Point[] points_temp){

        boolean cont_line = false;
        boolean cont_complete = false;
        StringBuilder point_line = new StringBuilder();

        for(int c = 0; c < points_temp.length-2; c++){
            if((points_temp[c].SLOPE_ORDER.compare(points_temp[c+1], points_temp[c+2]) == 0)
                    && (main_point.SLOPE_ORDER.compare(points_temp[c+2], points_temp[c]) == 0)){
                if (!cont_line) {
                    cont_line = true;
                    point_line.append(main_point).append(" -> ").append(points_temp[c]).append(" -> ").append(points_temp[c + 1]).append(" -> ").append(points_temp[c + 2]);
                    cont_complete = false;
                } else {
                    point_line.append(" -> ").append(points_temp[c + 2]);
                    if (c == points_temp.length-3){
                        cont_complete = true;
                    }
                }
            } else {
                cont_complete = true;
            }
            if (cont_complete && !point_line.toString().equals("")) {
                StdOut.println(point_line.toString());
                point_line = new StringBuilder();
                cont_line = false;
                cont_complete = false;
            }
        }
        if(!point_line.toString().equals("")){
            StdOut.println(point_line);
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
        Fast force = new Fast(points);

    }
}
