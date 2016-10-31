using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additonal Namespaces
using System.ComponentModel;
using ChinookSystem.Data.Entities;
using ChinookSystem.Data.POCOs;
using ChinookSystem.DAL;
#endregion

namespace ChinookSystem.BLL
{
    [DataObject]
    public class TrackController
    {
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List<Track> ListTracks()
        {
            using (var context = new ChinookContext())
            {
                //return all records, all attributes
                return context.Tracks.ToList();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public Track Get_Track(int trackid)
        {
            using (var context = new ChinookContext())
            {
                //return a records, all attributes
                return context.Tracks.Find(trackid);
            }
        }

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public void AddTrack(Track trackInfo)
        {
            using (var context = new ChinookContext())
            {
                //Any buisness rules

                //Any data refinements
                //Review of using iif (Imediate if)
                //Composer can be a null string, we do not wish to store an empty string
                trackInfo.Composer = string.IsNullOrEmpty(trackInfo.Composer) ? null : trackInfo.Composer;

                //Add the instance of track info to the database
                context.Tracks.Add(trackInfo);

                //Commit transaction
                context.SaveChanges();
            }
        }

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public void UpdateTrack(Track trackInfo)
        {
            using (var context = new ChinookContext())
            {
                //Any buisness rules

                //Any data refinements
                //Review of using iif (Imediate if)
                //Composer can be a null string, we do not wish to store an empty string
                trackInfo.Composer = string.IsNullOrEmpty(trackInfo.Composer) ? null : trackInfo.Composer;

                //Update the existing instance on database
                context.Entry(trackInfo).State = System.Data.Entity.EntityState.Modified;

                //Commit transaction
                context.SaveChanges();
            }
        }

        //The delete is an overloaded method technique
        [DataObjectMethod(DataObjectMethodType.Delete,true)]
        public void DeleteTrack(Track trackinfo)
        {
            DeleteTrack(trackinfo.TrackId);
        }

        public void DeleteTrack(int trackid)
        {
            using (var context = new ChinookContext())
            {
                //Any buisness rules

                //Do the delete, find the existing record on the database
                var existing = context.Tracks.Find(trackid);
                //Or you can do:
                //var existing = GetTrack(trackid);

                //Delete the record from the database
                context.Tracks.Remove(existing);

                //Commit transaction
                context.SaveChanges();
            }
        }
    }
}
