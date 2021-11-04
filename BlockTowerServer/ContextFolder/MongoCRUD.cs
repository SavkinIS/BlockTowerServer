using BlockTowerServer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlockTowerServer.DataBase
{
    public class MongoCRUD
    {
        IMongoDatabase db;


        public MongoCRUD(string database)
        {
            db = new MongoClient("mongodb+srv://Good:123456789Q@cluster0.om6xa.mongodb.net/Good?retryWrites=true&w=majority").GetDatabase(database);
                
        }

        /// <summary>
        /// Добавляем запись
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="record"></param>
        public void InsertRecord<T>(string table, T record)
        {
            db.GetCollection<T>(table).InsertOne(record);
        }

        /// <summary>
        /// Добавляем записи
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="records"></param>
        public void InsertRecord<T>(string table, List<T> records)
        {
            db.GetCollection<T>(table).InsertMany(records);
        }


        /// <summary>
        /// Получаем запись из введенной таблицы
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public T LoadRecord<T>(string table, int id)
        {
            return db.GetCollection<T>(table).Find(Builders<T>.Filter.Eq("Id", id)).First();
        }


        public List<T> LoadRecords<T>(string table)
        {
            return db.GetCollection<T>(table).Find(new BsonDocument()).ToList();
        }



        /// <summary>
        /// Измениение записи
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <param name="id"></param>
        /// <param name="record"></param>
        public void UpdateRecord<T>(string table, int id, T record)
        {
            BsonDocument filter = new BsonDocument("_id", value: id);
            ReplaceOptions replace = new ReplaceOptions { IsUpsert = false };
            db.GetCollection<T>(table).ReplaceOne(filter, record, replace);
        }

        public void DeleteRecord<T>(string table, int id)
        {
            db.GetCollection<T>(table)
                .DeleteOne(Builders<T>.Filter.Eq("Id", id));
        }

    }
}
