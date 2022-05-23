
using blogapi.Model;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace subject.Controllers
{
    [ApiController]
    public class subject : ControllerBase
    {

        string conqry = @"Data Source=VISHAL\SQLEXPRESS;Initial Catalog=blog;Integrated Security=True;Connect Timeout=3600;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        [HttpPost("api/subject/add")]
        public dynamic AddSubject(subjectreq req)
        {
            using (SqlConnection con = new SqlConnection(conqry))
            {
                string qry = "INSERT INTO subject(subject_name) VALUES('" + req.subject_name + "')";
                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Ok("insert Successfully");
        }

        [HttpPost("api/subject/update")]
        public dynamic UpdateSubject(subjectreq req)
        {
            using (SqlConnection con = new SqlConnection(conqry))
            {
                string qry = "UPDATE subject SET subject_name='" + req.subject_name + "' where sid='" + req.sid + "'";

                using (SqlCommand cmd = new SqlCommand(qry, con))
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            return ("update Successfully");
        }

        [HttpDelete("api/subject/delete")]
        public dynamic DeleteSubject([FromQuery] int sid)
        {
            using (SqlConnection con = new SqlConnection(conqry))
            {
                string qry = "DELETE FROM subject where sid=" + sid;
                using (SqlCommand command = new SqlCommand(qry, con))
                {
                    con.Open();
                    command.ExecuteNonQuery();
                    con.Close();
                }
            }
            return Ok("Deleted...");

        }

        [HttpGet("api/subject/list")]
        public dynamic GetSubjectList()
        {
            DataTable dtDepart = Getsubject();
            List<subjectreq> subList = new List<subjectreq>();
            foreach (DataRow drDepart in dtDepart.Rows)
            {
                subList.Add(new subjectreq()
                {
                    sid = System.Convert.ToInt32(drDepart["sid"]),
                    subject_name = drDepart["subject_name"].ToString()

                });
            }
            return Ok(subList);
        }
        private DataTable Getsubject()
        {
            DataTable dtDepart = new DataTable();
            using (SqlConnection con = new SqlConnection(conqry))
            {
                string qry = "select * from subject";
                using (SqlCommand command = new SqlCommand(qry, con))
                {
                    SqlDataAdapter adaptor = new SqlDataAdapter(command);
                    con.Open();
                    adaptor.Fill(dtDepart);
                    con.Close();
                }
            }
            return dtDepart;
        }

        [HttpGet("api/subject/detail")]
        public async Task<subjectreq> Getsubjectdetail([FromQuery] int sid)
        {
            var subject = new subjectreq();
            using (SqlConnection con = new SqlConnection(conqry))
            {
                con.Open();
                string sql = "Select * from subject where sid=" + sid;
                using (SqlCommand comm = new SqlCommand(sql, con))
                {
                    comm.Parameters.AddWithValue("sid", sid);
                    var reader = await comm.ExecuteReaderAsync();
                    int id = reader.GetOrdinal("sid");
                    int subject_name = reader.GetOrdinal("subject_name");
                    while (reader.Read())
                    {
                        subject.sid = reader.GetInt32(sid);
                        subject.subject_name = reader.GetString(subject_name);
                    }

                    return (subject);
                }
            }

        }

    }
}
