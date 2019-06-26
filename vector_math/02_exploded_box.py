import Rhino

#get the brep center
area = Rhino.Geometry.AreaMassProperties.Compute(box)
box_center = area.Centroid

#get a list of faces
faces = box.Faces

#decalre variables
exploded_faces = []

#loop through all faces
for i, face in enumerate(faces):
    
    #get a duplicate of the face
    extracted_face = faces.ExtractFace(i)
    
    #get the center of each face
    area = Rhino.Geometry.AreaMassProperties.Compute(extracted_face)
    center = area.Centroid
    
    #calculate move direction (from box centroid to face center)
    dir = center - box_center
    dir.Unitize()
    dir *= dis
    
    #move the extracted face
    move = Rhino.Geometry.Transform.Translation(dir)
    extracted_face.Transform(move)
    
    #add to exploded_faces list
    exploded_faces.append(extracted_face)
 
#assign exploded list of faces to output
a = exploded_faces