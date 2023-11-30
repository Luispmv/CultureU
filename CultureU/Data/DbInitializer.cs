using CultureU.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CultureU.Data
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolContext context)
        {
            // Look for any Alumnos.
            if (context.Alumnos.Any())
            {
                return;   // DB has been seeded
            }

            var alexander = new Alumno
            {
                FirstMidName = "Carson",
                LastName = "Alexander",
                InscripcionDate = DateTime.Parse("2016-09-01")
            };

            var alonso = new Alumno
            {
                FirstMidName = "Meredith",
                LastName = "Alonso",
                InscripcionDate = DateTime.Parse("2018-09-01")
            };

            var anand = new Alumno
            {
                FirstMidName = "Arturo",
                LastName = "Anand",
                InscripcionDate = DateTime.Parse("2019-09-01")
            };

            var barzdukas = new Alumno
            {
                FirstMidName = "Gytis",
                LastName = "Barzdukas",
                InscripcionDate = DateTime.Parse("2018-09-01")
            };

            var li = new Alumno
            {
                FirstMidName = "Yan",
                LastName = "Li",
                InscripcionDate = DateTime.Parse("2018-09-01")
            };

            var justice = new Alumno
            {
                FirstMidName = "Peggy",
                LastName = "Justice",
                InscripcionDate = DateTime.Parse("2017-09-01")
            };

            var norman = new Alumno
            {
                FirstMidName = "Laura",
                LastName = "Norman",
                InscripcionDate = DateTime.Parse("2019-09-01")
            };

            var olivetto = new Alumno
            {
                FirstMidName = "Nino",
                LastName = "Olivetto",
                InscripcionDate = DateTime.Parse("2011-09-01")
            };

            var Alumnos = new Alumno[]
            {
                alexander,
                alonso,
                anand,
                barzdukas,
                li,
                justice,
                norman,
                olivetto
            };

            context.AddRange(Alumnos);

            var abercrombie = new Profesor
            {
                FirstMidName = "Kim",
                LastName = "Abercrombie",
                HireDate = DateTime.Parse("1995-03-11")
            };

            var fakhouri = new Profesor
            {
                FirstMidName = "Fadi",
                LastName = "Fakhouri",
                HireDate = DateTime.Parse("2002-07-06")
            };

            var harui = new Profesor
            {
                FirstMidName = "Roger",
                LastName = "Harui",
                HireDate = DateTime.Parse("1998-07-01")
            };

            var kapoor = new Profesor
            {
                FirstMidName = "Candace",
                LastName = "Kapoor",
                HireDate = DateTime.Parse("2001-01-15")
            };

            var zheng = new Profesor
            {
                FirstMidName = "Roger",
                LastName = "Zheng",
                HireDate = DateTime.Parse("2004-02-12")
            };

            var Profesors = new Profesor[]
            {
                abercrombie,
                fakhouri,
                harui,
                kapoor,
                zheng
            };

            context.AddRange(Profesors);

            var Directivas = new Directiva[]
            {
                new Directiva {
                    Profesor = fakhouri,
                    Location = "Smith 17" },
                new Directiva {
                    Profesor = harui,
                    Location = "Gowan 27" },
                new Directiva {
                    Profesor = kapoor,
                    Location = "Thompson 304" }
            };

            context.AddRange(Directivas);

            var english = new Escuela
            {
                Name = "English",
                Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                Profesor = abercrombie
            };

            var mathematics = new Escuela
            {
                Name = "Mathematics",
                Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                Profesor = fakhouri
            };

            var engineering = new Escuela
            {
                Name = "Engineering",
                Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                Profesor = harui
            };

            var economics = new Escuela
            {
                Name = "Economics",
                Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                Profesor = kapoor
            };

            var Escuelas = new Escuela[]
            {
                english,
                mathematics,
                engineering,
                economics
            };

            context.AddRange(Escuelas);

            var chemistry = new Materia
            {
                MateriaID = 1050,
                Title = "Chemistry",
                Credits = 3,
                Escuela = engineering,
                Profesores = new List<Profesor> { kapoor, harui }
            };

            var microeconomics = new Materia
            {
                MateriaID = 4022,
                Title = "Microeconomics",
                Credits = 3,
                Escuela = economics,
                Profesores = new List<Profesor> { zheng }
            };

            var macroeconmics = new Materia
            {
                MateriaID = 4041,
                Title = "Macroeconomics",
                Credits = 3,
                Escuela = economics,
                Profesores = new List<Profesor> { zheng }
            };

            var calculus = new Materia
            {
                MateriaID = 1045,
                Title = "Calculus",
                Credits = 4,
                Escuela = mathematics,
                Profesores = new List<Profesor> { fakhouri }
            };

            var trigonometry = new Materia
            {
                MateriaID = 3141,
                Title = "Trigonometry",
                Credits = 4,
                Escuela = mathematics,
                Profesores = new List<Profesor> { harui }
            };

            var composition = new Materia
            {
                MateriaID = 2021,
                Title = "Composition",
                Credits = 3,
                Escuela = english,
                Profesores = new List<Profesor> { abercrombie }
            };

            var literature = new Materia
            {
                MateriaID = 2042,
                Title = "Literature",
                Credits = 4,
                Escuela = english,
                Profesores = new List<Profesor> { abercrombie }
            };

            var Materias = new Materia[]
            {
                chemistry,
                microeconomics,
                macroeconmics,
                calculus,
                trigonometry,
                composition,
                literature
            };

            context.AddRange(Materias);

            var Inscripcions = new Inscripcion[]
            {
                new Inscripcion {
                    Alumno = alexander,
                    Materia = chemistry,
                    Grade = Grade.A
                },
                new Inscripcion {
                    Alumno = alexander,
                    Materia = microeconomics,
                    Grade = Grade.C
                },
                new Inscripcion {
                    Alumno = alexander,
                    Materia = macroeconmics,
                    Grade = Grade.B
                },
                new Inscripcion {
                    Alumno = alonso,
                    Materia = calculus,
                    Grade = Grade.B
                },
                new Inscripcion {
                    Alumno = alonso,
                    Materia = trigonometry,
                    Grade = Grade.B
                },
                new Inscripcion {
                    Alumno = alonso,
                    Materia = composition,
                    Grade = Grade.B
                },
                new Inscripcion {
                    Alumno = anand,
                    Materia = chemistry
                },
                new Inscripcion {
                    Alumno = anand,
                    Materia = microeconomics,
                    Grade = Grade.B
                },
                new Inscripcion {
                    Alumno = barzdukas,
                    Materia = chemistry,
                    Grade = Grade.B
                },
                new Inscripcion {
                    Alumno = li,
                    Materia = composition,
                    Grade = Grade.B
                },
                new Inscripcion {
                    Alumno = justice,
                    Materia = literature,
                    Grade = Grade.B
                }
            };

            context.AddRange(Inscripcions);
            context.SaveChanges();
        }
    }
}