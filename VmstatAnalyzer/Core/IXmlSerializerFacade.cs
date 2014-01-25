/*
 *  @Project		Core.Common
 *  @Name 			IXmlSerializerFacade.cs
 *  @Date			2009-12-01
 *  @Author 		Yoon Min-kyung
 *  @Company        JUNGDO UIT CO.,LTD.
 *  @Description	XML 직렬화 처리 기능을 정의한 인터페이스.
 */

namespace Core.Common.Serialization
{
    /// <summary>
    /// XML 직렬화 처리 기능을 정의한 인터페이스.
    /// </summary>
    public interface IXmlSerializerFacade
    {
        /// <summary>
        /// 직렬화 대상 객체.
        /// </summary>
        object Object { get; set; }

        /// <summary>
        /// XML 파일명.
        /// </summary>
        string Filename { get; set; }

        /// <summary>
        /// 직렬화 메서드.
        /// </summary>
        void Serialize();

        /// <summary>
        /// 역직렬화 메서드.
        /// </summary>
        void Deserialize();
    }
}
