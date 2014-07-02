/*
 *  @Project		Core.Common
 *  @Name 			XmlSerializerFacade.cs
 *  @Date			2009-12-01
 *  @Author 		Yoon Min-kyung
 *  @Company        JUNGDO UIT CO.,LTD.
 *  @Description	XML 직렬화 처리 기능을 정의한 클래스.
 */

using System;
using System.IO;
using System.Xml.Serialization;

namespace Core.Common.Serialization
{
    /// <summary>
    /// XML 직렬화 처리 기능을 정의한 클래스.
    /// </summary>
    public class XmlSerializerFacade : IXmlSerializerFacade
    {
        #region Constructors

        /// <summary>
        /// 생성자.
        /// </summary>
        /// <param name="type">직렬화 대상 객체의 Type.</param>
        public XmlSerializerFacade(Type type)
            : this(type, null, null)
        {
        }

        /// <summary>
        /// 생성자.
        /// </summary>
        /// <param name="type">직렬화 대상 객체의 Type.</param>
        /// <param name="filename">XML 파일명.</param>
        public XmlSerializerFacade(Type type, string filename)
            : this(type, filename, null)
        {
        }

        /// <summary>
        /// 생성자.
        /// </summary>
        /// <param name="type">직렬화 대상 객체의 Type.</param>
        /// <param name="obj">직렬화 대상 객체.</param>
        public XmlSerializerFacade(Type type, object obj)
            : this(type, null, obj)
        {
        }

        /// <summary>
        /// 생성자.
        /// </summary>
        /// <param name="type">직렬화 대상 객체의 Type.</param>
        /// <param name="filename">XML 파일명.</param>
        /// <param name="obj">직렬화 대상 객체.</param>
        public XmlSerializerFacade(Type type, string filename, object obj)
        {
            m_type = type;
            m_object = obj;

            if (string.IsNullOrEmpty(filename))
            {
                m_filename = FILENAME;
            }
            else
            {
                m_filename = filename;
            }

            InitializeSerializer(type);
        }

        #endregion

        #region Members

        /// <summary>
        /// 기본 파일명을 정의한 상수.
        /// "serial.xml".
        /// </summary>
        protected const string FILENAME = "serial.xml";

        /// <summary>
        /// 직렬화 대상 객체의 Type.
        /// </summary>
        protected Type m_type;

        /// <summary>
        /// XML 파일명.
        /// </summary>
        protected string m_filename;

        /// <summary>
        /// 직렬화 대상 객체.
        /// </summary>
        protected object m_object;

        /// <summary>
        /// XmlSerializer 객체.
        /// </summary>
        protected XmlSerializer m_serializer = null;

        #endregion

        #region Protected & Internal Methods

        /// <summary>
        /// XmlSerializer 객체 초기화.
        /// </summary>
        /// <param name="type">직렬화 대상 객체의 Type.</param>
        protected virtual void InitializeSerializer(Type type)
        {
            m_serializer = new XmlSerializer(type);
        }

        #endregion

        #region IXmlSerializerFacade 멤버

        /// <summary>
        /// 직렬화 대상 객체.
        /// </summary>
        public object Object
        {
            get { return m_object; }
            set { m_object = value; }
        }

        /// <summary>
        /// XML 파일명.
        /// </summary>
        public string Filename
        {
            get
            {
                return m_filename;
            }
            set
            {
                m_filename = value;
            }
        }

        /// <summary>
        /// 직렬화 메서드.
        /// </summary>
        public void Serialize()
        {
            // 직렬화 대상 객체가 null이면 새로 생성한다.
            if (m_object == null) { m_object = Activator.CreateInstance(m_type); }
   
            FileStream stream = new FileStream(m_filename, FileMode.Create);
            m_serializer.Serialize(stream, m_object);
            stream.Close();
        }

        /// <summary>
        /// 역직렬화 메서드.
        /// </summary>
        public void Deserialize()
        {
            // 파일이 존재하지 않으면 새로 생성한다.
            if (!File.Exists(m_filename)) { Serialize(); }

            FileStream stream = new FileStream(m_filename, FileMode.Open);
            m_object = m_serializer.Deserialize(stream);
            stream.Close();
        }

        #endregion
    }
}
