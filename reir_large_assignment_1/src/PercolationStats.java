import edu.princeton.cs.algs4.StdOut;
import edu.princeton.cs.algs4.StdRandom;
import edu.princeton.cs.algs4.StdStats;
import edu.princeton.cs.algs4.Stopwatch;
import java.lang.Math;

public class PercolationStats {

    private double[] timeArr;
    private double[] thresholdArray;
    private double thresholdMean;
    private double thresholdStddev;
    private double thresholdConfidenceLow;
    private double thresholdConfidenceHigh;
    private double timeMean;
    private double timeStddev;
    private double timeConfidenceLow;
    private double timeConfidenceHigh;
    private int times;
    private int n;


    public PercolationStats(int N, int T){
        times = T;
        n = N;
        if (N <= 0 || T <= 0) {
            throw new IllegalArgumentException();
        }
        timeArr = new double[T];
        thresholdArray = new double[T];
        for (int i = 0; i < T; i++) {
            Stopwatch time = new Stopwatch();
            Percolation perc = new Percolation(N);
            while(!perc.percolates()){
                int row = StdRandom.uniform(N);
                int col = StdRandom.uniform(N);
                perc.open(row, col);
            }
            int openNumber = perc.numberOfOpenSites();
            double threshold = (double)openNumber / (double)(N * N);
            timeArr[i] = time.elapsedTime();
            thresholdArray[i] = threshold;
        }
        // when n is low, Stopwatch won't always record the time, this loop replaces
        // 0.0 with a low number so that confidence low doesn't become a negative number
        for (int i = 0; i < timeArr.length; i++){
            if (timeArr[i] == 0.0){
                timeArr[i] = 0.001;
            }
        }
        thresholdMean = StdStats.mean(thresholdArray);
        thresholdStddev = StdStats.stddev(thresholdArray);
        thresholdConfidenceLow = thresholdMean - (1.96*thresholdStddev/Math.sqrt(times));
        thresholdConfidenceHigh = thresholdMean + (1.96*thresholdStddev/Math.sqrt(times));
        timeMean = mean();
        timeStddev = stddev();
        timeConfidenceLow = confidenceLow();
        timeConfidenceHigh = confidenceHigh();

        }

    public double mean()
    {
        return StdStats.mean(timeArr);
    }

    public void printResults(){
        StdOut.println("T=" + times + " N=" + n);
        StdOut.println("thresh_Mean: " + thresholdMean);
        StdOut.println("thresh_Standard_deviation: " + thresholdStddev);
        StdOut.println("thresh_Confidence_Low: " + thresholdConfidenceLow);
        StdOut.println("thresh_Confidence_High: " + thresholdConfidenceHigh);
        StdOut.println("time_Mean: " + timeMean);
        StdOut.println("time_Standard_deviation: " + timeStddev);
        StdOut.println("time_Confidence_Low: " + timeConfidenceLow);
        StdOut.println("time_Confidence_High: " + timeConfidenceHigh);
    }
    public double stddev()
    {
        return StdStats.stddev(timeArr);
    }
    public double confidenceLow()
    {
        return timeMean - (1.96*timeStddev/Math.sqrt(times));
    }
    public double confidenceHigh()
    {
        return timeMean + (1.96*timeStddev/Math.sqrt(times));
    }

    public static void main(String[] args) {
        PercolationStats stats = new PercolationStats(40, 4);
        stats.printResults();


    }
}

