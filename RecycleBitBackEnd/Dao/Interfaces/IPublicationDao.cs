namespace RecycleBitBackEnd.Dao.Interfaces {

    public interface IPublicationDao {

        void CreatePublication(object publication);

        void EditPublication(object publication);

        void CancelPublication(int idPublication);

        void getAllPublication();

        void getPublicationById();
    }
}