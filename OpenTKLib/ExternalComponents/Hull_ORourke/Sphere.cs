/*
-------------------------------------------------------------------------
Class Sphere 
equivalent to its homonymous in C.
-------------------------------------------------------------------------
This program will generate a given number of points uniformly distributed
on the surface of a sphere. The number of points is given on the command
line as the first parameter.  Thus `sphere 100' will generate 100 points 
on the surface of a sphere, and output them to stdout.
	A number of different command-line flags are provided to set the 
radius of the sphere, control the output format, or generate points on 
an ellipsoid.  The definition of the flags is printed if the program is 
run without arguments: `sphere'.
	The idea behind the algorithm is that for a sphere of radius r, the 
area of a zone of width h is always 2*pi*r*h, regardless of where the sphere 
is sliced.  The implication is that the z-coordinates of random points on a 
sphere are uniformly distributed, so that x and y can always be generated by 
a given z and a given angle.
	The default output is integers, Rounded from the floating point 
computation.  The Rounding implies that some points will fall outside
the sphere, and some inside.  If all are required to be inside, then
the calls to the cast (int) should be removed.  
	The flags -a, -b, -c are used to set ellipsoid axis Lengths.  
Note that the points are not uniformly distributed on the ellipsoid: they 
are uniformly distributed on the sphere and that is scaled to an ellipsoid.
	Random.NextDouble() is used to generate random numbers, automatically 
seeded with time by Java.

Reference: J. O'Rourke, Computational Geometry Column 31,
Internat. J. Comput. Geom. Appl. 7 379--382 (1997);
Also in SIGACT News, 28(2):20--23 (1997), Issue 103.

Written by Joseph O'Rourke and Min Xu, June 1997.
Used in the textbook, "Computational Geometry in C."
Questions to orourke@cs.smith.edu.
--------------------------------------------------------------------
This code is Copyright 1997 by Joseph O'Rourke.  It may be freely
redistributed in its entirety provided that this copyright notice is
not removed.
--------------------------------------------------------------------
*/


using OpenTK;
using OpenTKExtension;
using System;

namespace OpenTKExtension
{
    public class Sphere
{


        private float r1, r2, r3, r;
private bool float_pt = false;

        
 public static void TestSphere()
 {

     string[] args = null;
     Sphere sph = new Sphere(args);
  }


public void print_instruct()
{
  System.Diagnostics.Debug.WriteLine ( "Please enter your input according to the following format:" );
  System.Diagnostics.Debug.WriteLine ( "\tsphere [number of points] [-flag letter][parameter value] " );
  System.Diagnostics.Debug.WriteLine ( "\t\t (Please put a space between flag letter and parameter value!) " );
  System.Diagnostics.Debug.WriteLine ( "Available flags are: " );
  System.Diagnostics.Debug.WriteLine ( "\t-r[parameter] \t set radius of the sphere (default: 100) " );
  System.Diagnostics.Debug.WriteLine ( "\t-f            \t set output to floating point format (default: integer) ");
  System.Diagnostics.Debug.WriteLine ( "\t-a[parameter] \t ellipsoid x-axis Length (default: sphere radius) ");
  System.Diagnostics.Debug.WriteLine ( "\t-b[parameter] \t ellipsoid y-axis Length (default: sphere radius) ");
  System.Diagnostics.Debug.WriteLine ( "\t-c[parameter] \t ellipsoid z-axis Length (default: sphere radius) ");
}




public Sphere(String[] args)

{

  int n;		/* number of points */
  float x, y, z, w, t;
  float R = 100.0f;	/* default radius */
  int r1a, r2a, r3a;

  if ( args.Length < 1 )
    {
    print_instruct();
    return;
    }

  r = (int) R;
  r1 = r2 = r3 = r;
  TestFlags (args);
  n = 1;

  
  try 
    {
    int.TryParse( args[0] , out n);
    }
  catch (Exception err) 
  {
      System.Diagnostics.Debug.WriteLine ("Invalid number of poits: " + err.Message); 
      return;
  }
  

     System.Random myRandom = new System.Random (); 


  while (n-->0){
    /* Generate a random point on a sphere of radius 1. */
    /* the sphere is sliced at z, and a random point at angle t
       generated on the circle of intersection. */
    z = 2*  Convert.ToSingle(myRandom.NextDouble()) - 1.0f;
    t = 2* Convert.ToSingle( Math.PI * myRandom.NextDouble());
    w =Convert.ToSingle( Math.Sqrt( 1 - z*z ));
    x = Convert.ToSingle(w * Math.Cos( t ));
    y = Convert.ToSingle(w * Math.Sin( t ));
    
    if ( float_pt == false )
      {
	r1a =(int) Math.Round(r1 * x);
	r2a =(int) Math.Round(r2 * y);
	r3a =(int) Math.Round(r3 * z);
	System.Diagnostics.Debug.WriteLine(r1a+"\t"+r2a+"\t"+r3a);
      }
    else
      System.Diagnostics.Debug.WriteLine((r1*x)+"\t"+(r2*y)+"\t"+(r3*z)) ;
   
  }
 System.Diagnostics.Debug.WriteLine("end");
}


public void TestFlags (String[] args)
{

  int i = 1;

  /* Test for flags */
  while ( i < args.Length) {

    /* Test for radius flag */
    
    if (args[i].Equals("-r")) 
      {
	i++;
	try 
	  {
	  float.TryParse(args[i], out r);
	    }
	  catch (Exception e) {System.Diagnostics.Debug.WriteLine("Please enter a valid number for the Radius: " + e.Message); return;}
	if (r==0) {
	  System.Diagnostics.Debug.WriteLine ( "Invalid radius flag " );
	  return;
        
	}
	else
	  r1 = r2 = r3 = r;
	 System.Diagnostics.Debug.WriteLine("r, r1, r2, r3 are: "+r+r1+r2+r3);
      }
    

    /* Test whether user wants floating point output */
    if (args[i].Equals("-f"))
      float_pt = true;

    /* Test for ellipsoid radius if any */
    if (args[i].Equals("-a")) 
      {
	i++;
	  try{
          float.TryParse(args[i], out r1);
	    }
	  catch (Exception e) 
      {
          System.Diagnostics.Debug.WriteLine("Please enter a valid number for the Radius: " + e.Message); 
          return;
      }
	if (r==0) {
	  System.Diagnostics.Debug.WriteLine ( "Invalid radius flag " );
	  return;
	}
      }

 if (args[i].Equals("-b")) 
      {
	i++;
	  try{ 
	  float.TryParse(args[i], out r2);
	    }
	  catch (Exception e) {System.Diagnostics.Debug.WriteLine("Please enter a valid number for the Radius: " + e.Message); return;}
	if (r==0) {
	  System.Diagnostics.Debug.WriteLine ( "Invalid radius flag " );
	  return;
	}
      }
 if (args[i].Equals("-c")) 
      {
	i++;
	  try
	    { 
	  float.TryParse(args[i], out r3);
	    }
	  catch (Exception e) {System.Diagnostics.Debug.WriteLine("Please enter a valid number for the Radius: " + e.Message); return;}
	if (r==0) {
	  System.Diagnostics.Debug.WriteLine ( "Invalid radius flag " );
	  return;
	}
      }

    i++;
  }

  if ( r1 == 0 || r2 == 0 || r3 == 0 )
    {
    System.Diagnostics.Debug.WriteLine ( "Invalid ellipsoid radius " );
    return;
    }
}


}
}