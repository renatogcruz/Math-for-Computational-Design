using Rhino;
using Rhino.Geometry;
using Rhino.DocObjects;
using Rhino.Collections;

using GH_IO;
using GH_IO.Serialization;
using Grasshopper;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Data;
using Grasshopper.Kernel.Types;

using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Runtime.InteropServices;



/// <summary>
/// This class will be instantiated on demand by the Script component.
/// </summary>
public class Script_Instance : GH_ScriptInstance
{
#region Utility functions
  /// <summary>Print a String to the [Out] Parameter of the Script component.</summary>
  /// <param name="text">String to print.</param>
  private void Print(string text) { /* Implementation hidden. */ }
  /// <summary>Print a formatted String to the [Out] Parameter of the Script component.</summary>
  /// <param name="format">String format.</param>
  /// <param name="args">Formatting parameters.</param>
  private void Print(string format, params object[] args) { /* Implementation hidden. */ }
  /// <summary>Print useful information about an object instance to the [Out] Parameter of the Script component. </summary>
  /// <param name="obj">Object instance to parse.</param>
  private void Reflect(object obj) { /* Implementation hidden. */ }
  /// <summary>Print the signatures of all the overloads of a specific method to the [Out] Parameter of the Script component. </summary>
  /// <param name="obj">Object instance to parse.</param>
  private void Reflect(object obj, string method_name) { /* Implementation hidden. */ }
#endregion

#region Members
  /// <summary>Gets the current Rhino document.</summary>
  private readonly RhinoDoc RhinoDocument;
  /// <summary>Gets the Grasshopper document that owns this script.</summary>
  private readonly GH_Document GrasshopperDocument;
  /// <summary>Gets the Grasshopper script component that owns this script.</summary>
  private readonly IGH_Component Component;
  /// <summary>
  /// Gets the current iteration count. The first call to RunScript() is associated with Iteration==0.
  /// Any subsequent call within the same solution will increment the Iteration count.
  /// </summary>
  private readonly int Iteration;
#endregion

  /// <summary>
  /// This procedure contains the user code. Input parameters are provided as regular arguments,
  /// Output parameters as ref arguments. You don't have to assign output parameters,
  /// they will have a default value.
  /// </summary>
  private void RunScript(Brep box, double dis, ref object A)
  {

    //get the brep center
    Rhino.Geometry.AreaMassProperties area = Rhino.Geometry.AreaMassProperties.Compute(box);
    Point3d box_center = area.Centroid;

    //get a list of faces
    Rhino.Geometry.Collections.BrepFaceList faces = box.Faces;

    //decalre variables
    Point3d center;
    Vector3d dir;
    List<Rhino.Geometry.Brep> exploded_faces = new List<Rhino.Geometry.Brep>();

    //loop through all faces
    for( int i = 0; i < faces.Count(); i++ )
    {
      //extract each of the face
      Rhino.Geometry.Brep extracted_face = box.Faces.ExtractFace(i);

      //get the center of each face
      area = Rhino.Geometry.AreaMassProperties.Compute(extracted_face);
      center = area.Centroid;

      //calculate move direction (from box centroid to face center)
      dir = center - box_center;
      dir.Unitize();
      dir *= dis;

      //move the extracted face
      extracted_face.Transform(Transform.Translation(dir));

      //add to exploded_faces list
      exploded_faces.Add(extracted_face);
    }

    //assign exploded list of faces to output
    A = exploded_faces;
  }

  // <Custom additional code> 

  // </Custom additional code> 
}