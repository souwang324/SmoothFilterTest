using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kitware.VTK;


namespace SmoothFilterTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SmoothFilterTest1();
        }
        public static void SmoothFilterTest1()
        {
            vtkSTLReader pSTLReader = vtkSTLReader.New();
            pSTLReader.SetFileName("../../../../res/cow.stl");
            pSTLReader.Update();

            vtkWindowedSincPolyDataFilter smoothFilter = vtkWindowedSincPolyDataFilter.New();
            smoothFilter.SetInputConnection(pSTLReader.GetOutputPort());
            smoothFilter.SetNumberOfIterations(100);
            smoothFilter.Update();

            vtkPolyDataMapper mapper = vtkPolyDataMapper.New();
            mapper.SetInputConnection(smoothFilter.GetOutputPort());
            //mapper.SetInput(normals.GetOutput());

            vtkActor actor = vtkActor.New();
            actor.SetMapper(mapper);

            vtkRenderer renderer = vtkRenderer.New();
            renderer.AddActor(actor);
            renderer.SetBackground(.1, .2, .3);
            renderer.ResetCamera();

            vtkRenderWindow renderWin = vtkRenderWindow.New();
            renderWin.AddRenderer(renderer);

            vtkRenderWindowInteractor interactor = vtkRenderWindowInteractor.New();
            interactor.SetRenderWindow(renderWin);

            renderWin.Render();
            interactor.Start();
        }
    }
}
