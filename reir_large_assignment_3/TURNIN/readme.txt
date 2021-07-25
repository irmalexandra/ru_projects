/**********************************************************************
 *  readme.txt template                                                   
 *  Kd-tree
**********************************************************************/

Name: Loki Alexander Hopkins
Login: lokih
Section instructor: Einar Helgi Guðmundsson

Partner name: Ríkharður Friðgeirsson
Partner login: rikhardur19
Partner section instructor: Einar Helgi Guðmundsson

Partner name: Emil Örn Kristjánsson
Partner login: emilk18
Partner section instructor: Einar Helgi Guðmundsson


/**********************************************************************
 *  Describe the Node data type you used to implement the
 *  2d-tree data structure.
 **********************************************************************/

    The Node contains the value in a Point2D data structure.
    It contains three Node structures for left child, right child and parent.
    And a boolean to tell if it is a vertical Node or a horizontal Node.


/**********************************************************************
 *  Describe your method for range search in a kd-tree.
 **********************************************************************/

    As parameters we send in a SET structure to keep track of the Nodes found in the rectangle,
    A RectHV structure as the requested rectangle to search in, the current Node being inspected,
    and finally the RectHV structure of the parent Node, so that we can create a rectangle for the current Node.

    We recursively create a RectHV data structure for each Node that is examined,
    using the Nodes point values as well as the Nodes parents rectangle values.

    We check every Node to see if the rectangle contains that point, and if so we add it to the SET data structure.
    Next we check if the rectangle of the current Node intersects with the primary rectangle, if so then we recursively
    check both children of the current Node, if not then we check the relevant x values of the rectangle if the current Node
    is vertical and y values otherwise to see if the current Nodes left or right child should be inspected.
    The left child should be inspected if the maximum x value or the maximum y value is less than the
    current Nodes x or y value, and the right child should be inspected if the minimum x value or minimum y value is more
    than the current Nodes x or y value.

    The method returns the SET structure of points within the rectangle if the current Node is null.


/**********************************************************************
 *  Describe your method for nearest neighbor search in a kd-tree.
 **********************************************************************/

    As parameters we send in the current Node being inspected, the Node that has the smallest distance to the
    destination point so far of the Nodes that have been inspected, the Point2D destination as
    well as a RectHV of the parent Node.

    Like with range, we recursively create a RectHV data structure for each Node that is examined,
    using the Nodes point values as well as the Nodes parents rectangle values.

    We check if the distance between the nearest Nodes point and the destination point is less than the distance between the
    current nearest Nodes point and the destination point, if it is then the nearest Node is updated to the current Node.
    Next we check if the distance between the current Nodes rectangle and the destination is less
    than the nearest Nodes point to the destination, if it is then we recursively call both the left and right children
    of the current Node.

    If the distance between the left Node and the destination is less than the
    right Node to the destination and then we update the nearest Node to the closer of the two.

    The method returns the nearest Node if the current Node is null or if the distance between the
    current Nodes rectangle and the destination is greater than the nearest Nodes point and the destination, this second
    condition means that no children of the current Node will be closer to the destination than the nearest Point.


/**********************************************************************
 *  Give the total memory usage in bytes (using tilde notation and 
 *  the standard 64-bit memory cost model) of your 2d-tree data
 *  structure as a function of the number of points N. Justify your
 *  answer below.
 *
 *  Include the memory for all referenced objects (deep memory),
 *  including memory for the nodes, points, and rectangles.
 **********************************************************************/

    bytes per Point2D: 32 bytes

    bytes per RectHV: 32 bytes + 16 bytes (overhead)

    bytes per KdTree of N points (using tilde notation):
    f(N) = ~ ((33 bytes + 16 bytes (overhead)) * N) + 8 bytes + 4 bytes + 16 bytes (overhead)

    Each created Node is 49 bytes: 8 bytes for each reference to another Node (3), 8 bytes for a reference to a Point2D
    structure, and 1 byte for a boolean value, and 16 bytes for overhead.
    In addition we have a reference to an initial root Node (8 bytes) and an int value for the size of the KdTree (4 bytes)
    and the 16 bytes for overhead.


/**********************************************************************
 *  Give the expected running time in seconds (using tilde notation)
 *  to build a 2d-tree on N random points in the unit square.
 *  Use empirical evidence by creating a table of different values of N
 *  and the timing results. (Do not count the time to generate the N 
 *  points or to read them in from standard input.)
 **********************************************************************/

    Expected running time in seconds is ~N*log(N).
    This is because it should only take each insert an average of log(N) time to find its correct place due to the
    nature of binary trees.
    The running time is not higher because we do not create a rectangle for every point that is inserted.
    This running time is supported by table 1.

	+--------------------------------------+
	| Table 1: Time comparison in seconds  |
	+--+--+--+--+--+--+--+--+--+--+--+--+--+
	|       N      |  T  |    KdTree       |
	+--+--+--+--+--+--+--+--+--+--+--+--+--+
	|   1.000.000  | 100 |     ~1,0492     |
	|  10.000.000  | 10  |    ~17,3017     |
	|  15.000.000  | 10  |    ~27,3627     |
	|  25.000.000  | 10  |    ~49,2418     |
	|  50.000.000  | 10  |   ~109,6480     |
	| 100.000.000  | 2   |   ~267,9295     |
	| 125.000.000  | 2   |   ~358,1825     |
	+--------+-----------------------------+
    T = How many tests were run, result is an average of that time.
    Higher T values cause running time to be too long.


/**********************************************************************
 *  How many nearest neighbor calculations can your brute-force
 *  implementation perform per second for input100K.txt (100,000 points)
 *  and input1M.txt (1 million points), where the query points are
 *  random points in the unit square? Explain how you determined the
 *  operations per second. (Do not count the time to read in the points
 *  or to build the 2d-tree.)
 *
 *  Repeat the question but with the 2d-tree implementation.
 **********************************************************************/

	+-----------------------------------------+
	| Table 2: Calls to nearest() per second  |
	+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
	|  txt file     | brute force | 2d-tree   |
	+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
	| input100K.txt |    ~116     |  ~10836   |
	|   input1M.txt |     ~10     |   ~1259   |
	+--------+--------------------------------+

	We built both solutions with the text documents provided.
	Then we created a Stopwatch variable and ran a while loop until that stopwatch reached one second.
	Inside the while loop there was a call to nearest with a random value Point2D structure and a counter for how many
	loops ran until it stopped. The counter is what is seen in table 2.


/**********************************************************************
 *  Have you taken (part of) this course before:
 **********************************************************************/

    No, none of us have.


/**********************************************************************
 *  Known bugs / limitations.
 **********************************************************************/

    Our solution can only support N = 125.000.000 when running through IntelliJ due to memory overload.


/**********************************************************************
 *  Describe whatever help (if any) that you received.
 *  Don't include readings, lectures, and dæmatímar, but do
 *  include any help from people (including course staff, 
 *  classmates, and friends) and attribute them by name.
 **********************************************************************/

    Classmate Margrét Guðmundsdóttir assisted us with calculating memory usage.


/**********************************************************************
 *  Describe any serious problems you encountered.                    
 **********************************************************************/

    The biggest roadblock was avoiding storing a RectHV data structure within each Node for its respective point.
    This complicated the methods draw, nearest and range. Our solution as stated above was to create a temporary
    RectHV structure for every recursive method call in each method.


/**********************************************************************
 *  If you worked with partners, assert below that you followed
 *  the protocol as described on the assignment page. Give one
 *  sentence explaining what each of you contributed.
 **********************************************************************/

    Emil Örn did most of the work on the draw method, first without any use of rectangles, and then completed the method by
    creating a temporary rectangle for the current Node.

    Loki Alexander did the implementation of PointSET and the initial workings of the nearest and range methods.

    Ríkharður took part in practically all solutions, both doing a lot of work with the draw method, and in the other methods.

    For 100% of the project implementation, all 3 of us worked in unison, working on each solution together.


/**********************************************************************
 *  List any other comments here. Feel free to provide any feedback   
 *  on how much you learned from doing the assignment, and whether    
 *  you enjoyed doing it.                                             
 **********************************************************************/
