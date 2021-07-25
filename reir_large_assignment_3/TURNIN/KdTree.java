
/*************************************************************************
 *************************************************************************/

import edu.princeton.cs.algs4.*;

import java.awt.*;
import java.util.InputMismatchException;


/**
 * A data structure that builds a binary tree of points based on their position on a grid.
 */
public class KdTree {

    /**
     * A data type that keeps track of a Point2D type and whether or not that point is vertical or horizontal.
     * Also has a reference to the Nodes left and right children as well as its parent Node.
     */
    private static class Node {
        Node left;
        Node right;
        Node parent;
        Point2D value;
        boolean vertical;

        private Node (Point2D value, Node parent, boolean vertical){
            this.value = value;
            this.left = null;
            this.right = null;
            this.parent = parent;
            this.vertical = vertical;
        }
    }

    private Node root;
    private int size = 0;

    /**
     * The constructor of KdTree, has no function.
     */
    public KdTree() {

    }

    /**
     * Checks if the KdTree is empty by comparing root to null.
     * @return the bool from the comparison of root and null.
     */
    public boolean isEmpty() {
        return root == null;
    }

    /**
     * Checks the size of the KdTree by returning the size variable.
     * @return the integer value of the size variable.
     */
    public int size() {
        return size;
    }

    /**
     * Takes in point and if root is null, assigns the point to root.value, otherwise sends
     * the point into insert_into_position.
     * @param point is the point that is either assigned to root.value or send forward to insert_into_position.
     */
    public void insert(Point2D point) {
        if (root != null){
            insert_into_position(point);
        } else {
            root = new Node(point, null, true);
            size++;
        }
    }

    /**
     * Takes in point and loops through the KdTree comparing values x and y for current_node and the point until
     * it has found the correct position for the point.
     * @param point The point that is inserted into the KdTree by assigning it to Node.value.
     */
    private void insert_into_position(Point2D point) {
        Node current_node = root;
        Node prev_node = null;
        boolean vertical = true;
        boolean left = true;

        while (current_node != null){
            if (current_node.value.x() == point.x() && current_node.value.y() == point.y()){
                return;
            }
            prev_node = current_node;
            if (vertical) {
                left = point.x() < current_node.value.x();
                vertical = false;
            } else {
                left = point.y() < current_node.value.y();
                vertical = true;
            }
            if (left) {
                current_node = current_node.left;
            } else {
                current_node = current_node.right;
            }
        }
        size++;
        if (left){
            prev_node.left = new Node(point, prev_node, vertical);
        } else {
            prev_node.right = new Node(point, prev_node, vertical);
        }
    }

    /**
     * Takes in point and loops through the KdTree comparing values x and y for current_node and the point until it has
     * found an exact match for both x and y values, then returns the Node that matched the param point.
     * @param point The point that is compared to every Node that might contain the same values.
     * @return A boolean for if the incoming point matches a pre-existing point in the KdTree.
     */
    public boolean contains(Point2D point) {
        Node next_node = root;
        boolean vertical = true;
        boolean left;

        while (next_node != null){
            if (next_node.value.x() == point.x() && next_node.value.y() == point.y()) {
                return true;
            }
            if (vertical) {
                left = point.x() < next_node.value.x();
                vertical = false;
            } else {
                left = point.y() < next_node.value.y();
                vertical = true;
            }
            if (left) {
                next_node = next_node.left;
            } else {
                next_node = next_node.right;
            }
        }
        return false;
    }

    /**
     *  Displays a canvas with the line of every point drawn on it, as well as showing the points themselves.
     */
    public void draw() {
        StdDraw.setCanvasSize(1000,1000);
        StdDraw.setXscale(-0.05, 1.05);
        StdDraw.setYscale(-0.05, 1.05);
        StdDraw.rectangle(0.5,0.5,0.5,0.5);
        RectHV root_rect = new RectHV(0, 0, 1, 1);
        draw_recursive(root, root_rect);
    }

    /**
     * Draws a line through the point of current_node and uses parent_rect as a reference for where the line should stop,
     * recursively calls the current nodes left and right children.
     * @param current_node It contains the point currently being used to draw a line. The color of the line depends on
     *                     on the Nodes vertical bool.
     * @param parent_rect It provides necessary parameters for creating a rectangle for current_node, as well as being
     *                    used as a reference for the distance of the line being drawn.
     */
    private void draw_recursive(Node current_node, RectHV parent_rect){
        if(current_node != null){
            StdDraw.setPenColor(StdDraw.BLACK);
            //StdDraw.textLeft(current_node.value.x()+0.01, current_node.value.y()+0.02, current_node.value.toString());
            StdDraw.setPenRadius(0.01);
            StdDraw.point(current_node.value.x(), current_node.value.y());
//            StdDraw.circle(current_node.value.x(), current_node.value.y(), .007);
            StdDraw.setPenRadius();

            if(current_node.vertical){
                StdDraw.setPenColor(StdDraw.RED);
                StdDraw.line(current_node.value.x(), parent_rect.ymin(), current_node.value.x(), parent_rect.ymax());
            }
            else{
                StdDraw.setPenColor(StdDraw.BLUE);
                StdDraw.line(parent_rect.xmin(), current_node.value.y(), parent_rect.xmax(), current_node.value.y());
            }

            draw_recursive(current_node.left, get_point_rect(current_node.left, parent_rect));
            draw_recursive(current_node.right, get_point_rect(current_node.right, parent_rect));
        }
    }

    /**
     * Finds all points in the KdTree that fit within the x and y coordinates of the incoming rectangle parameter.
     * @param rect Is sent to range_recursive.
     * @return An iterable SET of points that fit within rect.
     */
    public Iterable<Point2D> range(RectHV rect) {
        RectHV root_rect = new RectHV(0, 0, 1, 1);
        SET<Point2D> points_in_rectangle = new SET<>();
        return range_recursive(points_in_rectangle, rect, root, root_rect);
    }

    /**
     * Recursively checks if the current Nodes point is contained within the incoming rectangle as well as comparing the
     * distance between the current Nodes point and the incoming rectangle.
     * @param points_in_rectangle A SET that can have points added to it.
     * @param rect The incoming rectangle that might have points in the tree within it.
     * @param current_node A node that is compared with the incoming rectangle.
     * @param parent_rect A rectangle created from the parent of current_node, that is used to create the current_node rectangle.
     * @return the set of points contained within the incoming rectangle.
     */
    private SET<Point2D> range_recursive(SET<Point2D> points_in_rectangle, RectHV rect, Node current_node, RectHV parent_rect){

        boolean in_left, in_right, both;

        if (current_node != null){
            RectHV new_rect = get_point_rect(current_node, parent_rect);

            if (rect.contains(current_node.value)){
                points_in_rectangle.add(current_node.value);
            }
            both = rect.intersects(new_rect);
            if (both) {
                in_left = in_right = true;
            }
            else {
                if (current_node.vertical) {
                    in_left = rect.xmax() < current_node.value.x();
                    in_right = rect.xmin() > current_node.value.x();
                } else {
                    in_left = rect.ymax() < current_node.value.y();
                    in_right = rect.ymin() > current_node.value.y();
                }
            }

            if (in_left){
                points_in_rectangle = range_recursive(points_in_rectangle, rect, current_node.left, new_rect);
            }
            if (in_right){
                points_in_rectangle = range_recursive(points_in_rectangle, rect, current_node.right, new_rect);
            }
        }
        return points_in_rectangle;
    }

    /**
     * Returns a point based on how close it is to the input parameter point.
     * @param point It is the nearest method is suppose to find the closest point to.
     * @return the closest point of all points in the KdTree to the parameter point.
     */
    public Point2D nearest(Point2D point){
        RectHV root_rect = new RectHV(0, 0, 1, 1);
        Node nearest_node = nearest_recursive(root, root, point, root_rect);
        return nearest_node.value;
    }

    /**
     * Recursively compares current_node and nearest_node to their distance to destination, updates nearest_node to
     * current_node if the distance is less for current_node.
     * @param current_node It is the current_node being inspected for its distance to destination.
     * @param nearest_node It is the node with the least amount of distance to destination at this point.
     * @param destination It is the nearest method is suppose to find the closest point to.
     * @param parent_rect It is the rectangle for the parent node of nearest_node, it is used to create a rectangle
     *                    for nearest_node.
     * @return the nearest Node to destination when it has reached the end of each branch that it needed to inspect.
     */
    private Node nearest_recursive(Node current_node, Node nearest_node, Point2D destination, RectHV parent_rect) {
        if (current_node != null) {

            RectHV new_rect = get_point_rect(current_node, parent_rect);

            if (current_node.value.distanceSquaredTo(destination) < nearest_node.value.distanceSquaredTo(destination)){
                nearest_node = current_node;
            }
            if (new_rect.distanceSquaredTo(destination) <= nearest_node.value.distanceSquaredTo(destination)) {
                Node left = nearest_recursive(current_node.left, nearest_node, destination, new_rect);
                Node right = nearest_recursive(current_node.right, nearest_node, destination, new_rect);
                if (left.value.distanceSquaredTo(destination) <= right.value.distanceSquaredTo(destination)){
                    nearest_node = left;
                } else {
                    nearest_node = right;
                }
            } else {
                return nearest_node;
            }
        }
        return nearest_node;
    }

    /**
     * Creates a rectangle from a node and its parents rectangle.
     * @param node It is the node that needs a rectangle created for it.
     * @param parent_rect Is the reference rectangle used to figure out the shape of the rectangle in creation.
     * @return a rectangle that
     */
    private RectHV get_point_rect(Node node, RectHV parent_rect){
        double rect_x_min;
        double rect_x_max;
        double rect_y_min;
        double rect_y_max;
        if (node == root){
            return parent_rect;
        }
        if(node == null){
            return parent_rect;
        }
        if(node.vertical){
            if(node == node.parent.left){
                rect_x_min = parent_rect.xmin();
                rect_x_max = parent_rect.xmax();
                rect_y_min = parent_rect.ymin();
                rect_y_max = node.parent.value.y();
            }
            else{
                rect_x_min = parent_rect.xmin();
                rect_x_max = parent_rect.xmax();
                rect_y_min = node.parent.value.y();
                rect_y_max = parent_rect.ymax();
            }
        }else{
            if(node == node.parent.left){
                rect_x_min = parent_rect.xmin();
                rect_x_max = node.parent.value.x();
                rect_y_min = parent_rect.ymin();
                rect_y_max = parent_rect.ymax();
            }
            else{
                rect_x_min = node.parent.value.x();
                rect_x_max = parent_rect.xmax();
                rect_y_min = parent_rect.ymin();
                rect_y_max = parent_rect.ymax();
            }
        }
        return new RectHV(rect_x_min, rect_y_min, rect_x_max, rect_y_max);
    }

    /**
     * Creates a KdTree based on a input argument and then prints out an integer that indicates how many nodes were
     * found that were nearest to a random point in 1 second.
     * @param args Are the input parameters.
     * @param n Is the size of the incoming point set.
     */
    public static void count_nearest(String[] args, int n){
        In input = new In(args[0]);
        int N = n;
        Point2D[] point_arr = new Point2D[N];
        KdTree new_tree = new KdTree();

        for(int i = 0; !input.isEmpty(); i++){
            double x = input.readDouble();
            double y = input.readDouble();
            point_arr[i] = new Point2D(x,y);
        }
        for (Point2D point2D : point_arr) {
            new_tree.insert(point2D);
        }

        int counter = 0;
        Stopwatch OneSecond = new Stopwatch();
        while (OneSecond.elapsedTime() < 1){
            new_tree.nearest(new Point2D(StdRandom.uniform(), StdRandom.uniform()));
            counter++;
        }
        StdOut.println(counter);
    }

    /**
     * This functions builds a KdTree with random points and times how long it takes, prints out the average results.
     * @param n It is the size of the tree.
     * @param t It determines how many tests are run to get an average result.
     */
    public static void build_tree(int n, int t){
        int N = n;
        int T = t;
        double[] timeArr = new double[T];
        KdTree our_kd_tree;
        for (int i = 0; i < timeArr.length; i++){
            our_kd_tree = new KdTree();
            Point2D[] point_arr = new Point2D[N];
            for (int j = 0; j < N; j++){
                double x = StdRandom.uniform();
                double y = StdRandom.uniform();
                point_arr[j] = new Point2D(x, y);
            }
            Stopwatch watch = new Stopwatch();
            for (int j = 0; j < point_arr.length; j++){
                our_kd_tree.insert(point_arr[j]);
            }
            timeArr[i] = watch.elapsedTime();
        }
        double timeMean = StdStats.mean(timeArr);
        StdOut.println("Building tree of "+N+" Size took "+timeMean+" seconds");
    }



    /*******************************************************************************
     * Test client
     ******************************************************************************/

    public static void main(String[] args) {
        In input = new In(args[0]);
        int n = input.readInt();
        KdTree kd_tree = new KdTree();
        for (int i = 0; i <= n; i++){
            kd_tree.insert(new Point2D(StdRandom.uniform(), StdRandom.uniform()));
        }
        kd_tree.draw();
    }
}
