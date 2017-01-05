using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PhotoGallery.DAL;
using PhotoGallery.Models;
using PhotoGallery;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace PhotoGallery.Tests.DAL
{
    [TestClass]
    public class UnitTest1
    {
        Mock<ArtContext> mock_context { get; set; }
        Mock<DbSet<Art>> mock_dbset { get; set; }
        List<Art> variable_datastore { get; set; }
        ArtRepository repo { get; set; }


        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<ArtContext>();
            mock_dbset = new Mock<DbSet<Art>>();
            variable_datastore = new List<Art>();//fake database
            repo = new ArtRepository(mock_context.Object);//use the dependancy Injection
                                                          //.Object returns the instance

            var queryable_list = variable_datastore.AsQueryable();//type change

            // Lie to LINQ make it think that our new Queryable List is a Database table.
            //the real dbset doesn't have Provider, Expression method...
            //return only return once, callback could be used for many times
            //IQuerable only used in LINQ
            mock_dbset.As<IQueryable<Art>>().Setup(m => m.Provider).Returns(queryable_list.Provider);//where data from
            mock_dbset.As<IQueryable<Art>>().Setup(m => m.Expression).Returns(queryable_list.Expression);//e.g. SQL query is an expression; a big expression could be seperate into two expressions
            mock_dbset.As<IQueryable<Art>>().Setup(m => m.ElementType).Returns(queryable_list.ElementType);//key words is a element type, e.g. SELECT, FROM; * simbal; table, 3 element type
            mock_dbset.As<IQueryable<Art>>().Setup(m => m.GetEnumerator()).Returns(() => queryable_list.GetEnumerator());//could loop over ordered

            //mock context return the mock_variable_table when someone calls the SavingVariableContext.charValueDb
            mock_context.Setup(c => c.ArtContext).Returns(mock_dbset.Object);

            //capture when use Add function, instead use variable_datastore
            mock_dbset.Setup(t => t.Add(It.IsAny<Art>())).Callback((Art a/*capture the variable sent*/) => variable_datastore.Add(a)/*add it to a list*/);
            mock_dbset.Setup(t => t.Remove(It.IsAny<Art>())).Callback((Art a) => variable_datastore.Remove(a));

        }

        [TestCleanup]
        public void ClearUp()
        {
            repo = null;
        }

    }
}
    

