import bpy
from mathutils import Matrix as Matrica

bpy.ops.object.transform_apply(scale = True, location = False, rotation = False) #pasirasot, kad naudosit scale matrica ar kaip ji ten vadinas
objektas = bpy.context.object #
M = objektas.matrix_world

def MoveScale(Scale, Cnt):
    m = 0
    dz = objektas.dimensions[2]
    for i in range(Cnt):
        naujas_objektas = objektas.copy()
        naujas_objektas.data = naujas_objektas.data.copy()
        dz = objektas.dimensions.z
        m += ((Scale+1)*dz*(Scale**i))/2
        M0 = M.Translation((0,0,m))
        S0 = M.Scale(Scale**(i+1),4)
        naujas_objektas.matrix_world = M @ M0 @ S0
        
        bpy.data.scenes[0].collection.objects.link(naujas_objektas)
    return
MoveScale(0.75,5)