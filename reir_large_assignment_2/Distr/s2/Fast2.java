package s2;

import edu.princeton.cs.algs4.In;
import edu.princeton.cs.algs4.StdOut;

import java.util.Arrays;

public class Fast2 {
    static Point[] points;
    private Point[][] first_lines;
    private int lines_index;

    public Fast2(Point[] points){
        int length = points.length;
        first_lines = new Point[1000][];
        lines_index = 0;

        for(int a = 0; a < length - 1; a++){
            Point[] points_temp = createCompareArray(a, points, length);
            Arrays.sort(points_temp, points[a].SLOPE_ORDER);
            findLines(points[a], points_temp, a);
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

    private void findLines(Point main_point, Point[] points_temp, int a){

        boolean cont_line = false;
        boolean cont_complete = false;
        boolean first_line = true;
        int line_index = 0;
        StringBuilder point_line = new StringBuilder();
        Point[] line = new Point[points.length];
        int wtf = 0;

        if (main_point.x == 15000 && main_point.y == 30000){
            StdOut.println();
        }

        for(int c = 0; c < points_temp.length-2; c++){
            wtf = c;
            if((points_temp[c].SLOPE_ORDER.compare(points_temp[c+1], points_temp[c+2]) == 0)
                    && (main_point.SLOPE_ORDER.compare(points_temp[c+2], points_temp[c]) == 0)){

                if (!cont_line) {
                    cont_line = true;
                    point_line.append(main_point).append(" -> ").append(points_temp[c]).append(" -> ").append(points_temp[c + 1]).append(" -> ").append(points_temp[c + 2]);
                    cont_complete = false;

                    if(first_line){
                        line[line_index] = main_point;
                        line_index ++;
                        line[line_index] = points_temp[c];
                        line_index ++;
                        line[line_index] = points_temp[c+1];
                        line_index ++;
                        line[line_index] = points_temp[c+2];
                        line_index ++;
                    }


                } else {
                    if (first_line){
                        line[line_index] = points_temp[c+2];
                        line_index ++;
                    }
                    point_line.append(" -> ").append(points_temp[c + 2]);


                    if (c == points_temp.length-3){
                        cont_complete = true;
                    }
                }
            } else {
                cont_complete = true;
                line_index = 0;
            }
            if (cont_complete && !point_line.toString().equals("")) {
                if(!first_line){
                    StdOut.println(point_line.toString());
                }
                point_line = new StringBuilder();
                cont_line = false;
                cont_complete = false;
                if(first_line && !first_line){
                    boolean duplicate = false;
                    if(first_lines[0] != null){
                        for(int i = 0; i < first_lines.length; i++){
                            if (first_lines[i] != null){
                                for(int j = 0; j < first_lines[i].length; j++){
                                    if (first_lines[i][j] != null) {
                                        for(int k = 0; k < line.length; k++){
                                            if(line[k] != null){
                                                if(line[k] == first_lines[i][j]){
                                                    duplicate = true;
                                                }
                                                else{
                                                    duplicate = false;
                                                }
                                            }
                                            else{
                                                break;
                                            }
                                        }
                                    }
                                    else{
                                        break;
                                    }
                                }
                            }
                            else{
                                break;
                            }
                        }
                    }
                    if (!duplicate){
                        first_lines[lines_index] = line;
                        StringBuilder first_line_str = new StringBuilder();
                        for(int j = 0; j <first_lines[lines_index].length; j ++){
                            if(first_lines[lines_index][j] != null) {
                                if (first_lines[lines_index][j+1] == null){
                                    first_line_str.append(first_lines[lines_index][j]);
                                }
                                else {
                                    first_line_str.append(first_lines[lines_index][j]).append(" -> ");
                                }

                            }
                            else {
                                break;
                            }
                        }
                        StdOut.println(first_line_str);
                        lines_index ++;

                        line = new Point[points.length];
                    }
                }
                first_line = false;
            }
        }
        if(!point_line.toString().equals("") && wtf > 1){
            StdOut.println(point_line);
        }
    }

    public static void main(String[] args) {
        In in = new In(args[0]);
        int n = in.readInt();
        points = new Point[n];
        for (int i = 0; i < n; i++) {
            int x = in.readInt(), y = in.readInt();
            points[i] = new Point(x, y);
        }
        Arrays.sort(points);
        Fast2 force = new Fast2(points);

    }
}
