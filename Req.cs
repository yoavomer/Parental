using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    public class Req
    {
        
        DateTime dateTime;
        string ReqType;
        NetworkStream clientsStream;
        List<Req> Reqs = null;

        /// <summary>
        /// constructors that gets the request time stream and type
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="clientsStream"></param>
        /// <param name="ReqType"></param>
        public Req(DateTime dateTime, NetworkStream clientsStream, string ReqType)
        {   
            this.dateTime = dateTime;
            this.clientsStream = clientsStream;
            this.ReqType = ReqType;
        }
        /// <summary>
        /// return object ReqType
        /// </summary>
        /// <returns></returns>
        private string RequestType()
        {
            return this.ReqType;
        }
        /// <summary>
        /// return object client stream
        /// </summary>
        /// <returns></returns>
        private NetworkStream clientStream()
        {
            return this.clientsStream;
        }
        /// <summary>
        /// return object time
        /// </summary>
        /// <returns></returns>
        private DateTime Time()
        {
            return this.dateTime;
        }
        /// <summary>
        /// checks wheather the req is approved
        /// if it is adds to the log list
        /// </summary>
        /// <returns></returns>
        public bool Possible()
        {
            if (Reqs!= null)
            {
                for (int i = 0; i < Reqs.LastIndexOf(null); i++)
                {
                    if (Reqs[i].IsEquals(this))
                    {
                        return ((Reqs[i].Time() - this.dateTime).TotalSeconds > 3);
                    }
                }
                Reqs.Add(this);
            }
            return true;
        }
        /// <summary>
        /// checks wheather two objects are equal
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        private bool IsEquals(Req req)
        {
            if ((req.clientStream() == this.clientsStream) && (req.RequestType() == this.ReqType))
            {
                return true;
            }
            return false;
        }


    }
}
