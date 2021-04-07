using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using CRUD_Angular.Models;

namespace CRUD_Angular.Controllers
{
    [EnableCors(origins: "http://mywebclient.azurewebsites.net", headers: "*", methods: "*")]
    public class StudentController : ApiController
    {
        private SqlConnection conn;
        private SqlDataAdapter adapter;
        // GET api/<controller>
        public IEnumerable<Student> Get()
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-E7SCDHU\SQLEXPRESS;Initial Catalog=nawab;Integrated Security=True");
            DataTable dt = new DataTable();
            var query = "select * from Student";
            adapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(query, conn)
            };
            adapter.Fill(dt);
            List<Student> students = new List<Student>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow student in dt.Rows)
                {
                    students.Add(new ReadStudent(student));
                }
            }
            return students;
        }

        // GET api/<controller>/5
        public IEnumerable<Student> Get(int id)
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-E7SCDHU\SQLEXPRESS;Initial Catalog=nawab;Integrated Security=True");
            DataTable dt = new DataTable();
            var query = "select * from Student where id=" + id;
            adapter = new SqlDataAdapter
            {
                SelectCommand = new SqlCommand(query, conn)
            };
            adapter.Fill(dt);
            List<Student> students = new List<Student>(dt.Rows.Count);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow student in dt.Rows)
                {
                    students.Add(new ReadStudent(student));
                }
            }
            return students;
        }

        // POST api/<controller>
        public string Post([FromBody]CreateStudent value)
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-E7SCDHU\SQLEXPRESS;Initial Catalog=nawab;Integrated Security=True");
            var query = "insert into student (f_name,m_name,l_name, address, birthDay, score) values (@f_name,@m_name,@l_name, @address, @birthDay, @score)";
            SqlCommand insert = new SqlCommand(query, conn);
            insert.Parameters.AddWithValue("@f_name", value.f_name);
            insert.Parameters.AddWithValue("@m_name", value.m_name);
            insert.Parameters.AddWithValue("@l_name", value.l_name);
            insert.Parameters.AddWithValue("@address", value.address);
            insert.Parameters.AddWithValue("@birthDay", value.birthDay);
            insert.Parameters.AddWithValue("@score", value.score);
            conn.Open();
            int kq = insert.ExecuteNonQuery();
            if (kq > 0)
            {
                return "Them thanh cong";
            } else
            {
                return "Them that bai";
            }
        }

        // PUT api/<controller>/5
        public string Put(int id, [FromBody]CreateStudent value)
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-E7SCDHU\SQLEXPRESS;Initial Catalog=nawab;Integrated Security=True");
            var query = "update student set f_name = @f_name,m_name = @m_name,l_name=@l_name, address=@address, birthDay=@birthDay, score=@score where id=" + id;
            SqlCommand insert = new SqlCommand(query, conn);
            insert.Parameters.AddWithValue("@f_name", value.f_name);
            insert.Parameters.AddWithValue("@m_name", value.m_name);
            insert.Parameters.AddWithValue("@l_name", value.l_name);
            insert.Parameters.AddWithValue("@address", value.address);
            insert.Parameters.AddWithValue("@birthDay", value.birthDay);
            insert.Parameters.AddWithValue("@score", value.score);
            conn.Open();
            int kq = insert.ExecuteNonQuery();
            if (kq > 0)
            {
                return "Sua thanh cong";
            }
            else
            {
                return "Sua that bai";
            }
        }

        // DELETE api/<controller>/5
        public string Delete(int id)
        {
            conn = new SqlConnection(@"Data Source=DESKTOP-E7SCDHU\SQLEXPRESS;Initial Catalog=nawab;Integrated Security=True");
            var query = "delete from student where id=" + id;
            SqlCommand insert = new SqlCommand(query, conn);
            conn.Open();
            int kq = insert.ExecuteNonQuery();
            if (kq > 0)
            {
                return "Xoa thanh cong";
            }
            else
            {
                return "Xoa that bai";
            }
        }
    }
}