
/****************************************************************************
 *  Compilation:  javac PointSET.java
 *  Execution:    
 *  Dependencies:
 *  Author:
 *  Date:
 *
 *  Data structure for maintaining a set of 2-D points, 
 *    including rectangle and nearest-neighbor queries
 *
 *************************************************************************/

import edu.princeton.cs.algs4.*;
import java.util.Iterator;

public class PointSET {
    // construct an empty set of points
    SET<Point2D> point_set;
    public PointSET() {
        point_set = new SET<>();
    }

    /**
     * Checks if the PointSET is empty by calling the .isEmpty() method of SET.
     * @return the bool SET.isEmpty().
     */
    public boolean isEmpty() {
        return point_set.isEmpty();
    }

    /**
     * Checks the size of PointSET by calling the .size() method of SET.
     * @return the integer of SET.size().
     */
    public int size() {
        return point_set.size();
    }

    /**
     * Calls the .add method of SET with the p parameter.
     * @param p is the point being added to point_set.
     */
    public void insert(Point2D p) {
        point_set.add(p);
    }

    /**
     * Takes a point and checks if point_set contains it.
     * @param p The point that is sent to .contains method of SET.
     * @return A boolean for if the incoming point matches a pre-existing point in the PointSET.
     */
    public boolean contains(Point2D p) {
        return point_set.contains(p);
    }

    /**
     *  Displays a canvas with the line of every point drawn on it, as well as showing the points themselves.
     */
    public void draw() {
        Iterator<Point2D> my_point_array = point_set.iterator();
        while (my_point_array.hasNext()){
            my_point_array.next().draw();
        }
    }

    /**
     * Creates an iterable set of all points and loops through them, checking each if rect contains it.
     * @param rect Is the rectangle being compared with each point in the set.
     * @return a SET of points that are within rect.
     */
    public Iterable<Point2D> range(RectHV rect) {
        SET<Point2D> points_in_rectangle = new SET<>();
        Iterator<Point2D> my_point_array = point_set.iterator();

        while (my_point_array.hasNext()){
            Point2D current_point = my_point_array.next();
            if (rect.contains(current_point)){
                points_in_rectangle.add(current_point);
            }
        }
        return points_in_rectangle;
    }

    /**
     * Creates an iterable set of all points and loops through them, checking how close each of them is to the parameter p,
     * updates the closes point continuously.
     * @param p Is the point that is being used to find the nearest point.
     * @return a point that is closest to the incoming p point.
     */
    public Point2D nearest(Point2D p) {
        Iterator<Point2D> my_point_array = point_set.iterator();

        Point2D current_point = my_point_array.next();
        double min_distance = p.distanceSquaredTo(current_point);
        Point2D closest_point = current_point;

        while (my_point_array.hasNext()){
            current_point = my_point_array.next();
            if (p.distanceSquaredTo(current_point) < min_distance){
                min_distance = p.distanceSquaredTo(current_point);
                closest_point = current_point;
            }
        }
        return closest_point;
    }

    /**
     * Creates a PointSET based on a input argument and then prints out an integer that indicates how many nodes were
     * found that were nearest to a random point in 1 second.
     * @param args Are the input parameters.
     * @param n Is the size of the incoming point set.
     */
    public static void count_nearest(String[] args, int n){
        In input = new In(args[0]);
        int N = n;
        Point2D[] point_arr = new Point2D[N];
        PointSET new_point_set = new PointSET();

        for(int i = 0; !input.isEmpty(); i++){
            double x = input.readDouble();
            double y = input.readDouble();
            point_arr[i] = new Point2D(x,y);
        }
        for (Point2D point2D : point_arr) {
            new_point_set.insert(point2D);
        }

        int counter = 0;
        Stopwatch OneSecond = new Stopwatch();
        while (OneSecond.elapsedTime() < 1){
            new_point_set.nearest(new Point2D(StdRandom.uniform(), StdRandom.uniform()));
            counter++;
        }
        StdOut.println(counter);
    }

    //********************** Main here ****************************************

    public static void main(String[] args) {
        int n = 100000;
        int t = 100;
        for (int i = 0; i < t; i++){
            PointSET.count_nearest(args, n);
        }
    }
}
