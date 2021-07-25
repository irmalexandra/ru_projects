Fall 2020
T-301-REIR, Reiknirit

S2: Pattern Recognition

(Loki Alexander Hopkins), (lokih@ru.is), (180892-2149)
(Ríkharður Friðgeirsson), (rikhardur19@ru.is), (240292-2469)
(Emil Örn Kristjánsson), (emilk18@ru.is), (200695-2169)

Group S2-43
September 26, 2020

TA: Einar Helgi Guðmundsson

//--------------------------------------------------------//

1. Implementation
	Brute force:
		the slopeTo function in Point.java accepts a second Point instance and finds the slope of those two points with 
		m = (y1 - y0)/(x1 - x0).
		If the slope is infinite, then Double.POSITIVE_INFINITY is returned, otherwise the slope is returned.
		compareTo accepts a second Point instance and checks if this owners y and x are higher than the other Points y and x.

		Brute.java was implemented by creating 4  loops nested within each other. Each inner loop starts with the index location of the previous loop + 1.
		In the inner most loop, 4 points are created from the integer values of the 4 loops,
		then we get the slope between point1 (outer most loop point) and all the other points and check if all of those are equal.
		If they are, that means we found a line.

	Sorting solution:
		Before running through all the points in fast.java, we throw the Point[] array into Arrays.sort() and then when we've chosen a primary point in the outer loop,
		we sort the rest of the numbers using Arrays.sort() based on their slope to the primary point, using the compareSlope class we created in Point.java

		We avoid permutations by only comparing points after the current primary points location in the points array,
		this is done by having the inner loop always start on the outer loops integer+1.

		We did not manage to find a way to eliminate sub segments of a line. Our idea was to record the first appearance of a given line.
		And compare the rest of the lines with the same starting point to that line. To see if the current line was a sub segment of the first recorded line.



2. Emperical Analysis

	+-------------------------------=------------+
	| Table 1: Time comparison in seconds        |
	+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
	|    N   |      brute      |    sorting      |
	+--+--+--+--+--+--+--+--+--+--+--+--+--+--+--+
	|   150  |       0.381     |      0.019      |
	|   200  |       0.701     |      0.031      |
	|   300  |       2.853     |      0.050      |
	|   400  |       8.176     |      0.084      |
	|   800  |     130.154     |      0.373      |
	|  1600  |    2240.925     |      0.871      |
	|  3200  |       N/A       |      1.702      |
	|  6400  |       N/A       |      5.206      |
	| 12800  |       N/A       |     20.145      |
	+--------+-----------------+-----------------+

	N/A means the compute time was too long to test.

	Brute: ~n^4

	Sorting: ~n^2 * log(n)

	Theoretical

		Brute:
			1/30 n (1 + n) (1 + 2 n) (-1 + 3 n + 3 n^2)
			f(n) = 4n
			# TODO

		Sorting:
		    # TODO


3.  About This Solution
	Have you taken (part of) this course before: No
	Hours to complete assignment (optional): 60


	3.1  Known Bugs / Limitations
		We are unable to eliminate sub segments.

	3.2  Help Received
		No help received.

	3.3  Problem Encountered
		Trying to prevent a shorter version of a line from being counted as a line was our biggest problem.

	3.4  Comments
		Solving Fast was enjoyable and we learned more about the importance of efficient algorithms.