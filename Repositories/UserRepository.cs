namespace React_Redux.Repositories {
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;
    using System.Threading.Tasks;
    using System;
    using Microsoft.Azure.Documents.Client;
    using Microsoft.Azure.Documents.Linq;
    using Microsoft.Azure.Documents;
    using React_Redux.Helper;

    public class UserRepository<T> : IUserRepository<T> where T : class {
        private readonly string Endpoint = Constants.Server.EndPoint;
        private readonly string Key = Constants.Server.Key;
        private readonly string DatabaseId = Constants.Server.DatabaseId;
        private readonly string CollectionId = "User";
        private DocumentClient client;

        public UserRepository () {
            this.client = new DocumentClient (new Uri (Endpoint), Key);
            CreateDatabaseIfNotExistsAsync ().Wait ();
            CreateCollectionIfNotExistsAsync ().Wait ();
        }

        public async Task<T> GetItemAsync (string id) {
            try {
                Document document = await client.ReadDocumentAsync (UriFactory.CreateDocumentUri (DatabaseId, CollectionId, id));
                return (T) (dynamic) document;
            } catch (DocumentClientException e) {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    return null;
                } else {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<T>> GetItemsAsync (Expression<Func<T, bool>> predicate) {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T> (
                    UriFactory.CreateDocumentCollectionUri (DatabaseId, CollectionId),
                    new FeedOptions { MaxItemCount = -1 })
                .Where (predicate)
                .AsDocumentQuery ();

            List<T> results = new List<T> ();
            while (query.HasMoreResults) {
                results.AddRange (await query.ExecuteNextAsync<T> ());
            }

            return results;
        }

        public async Task<T> FindItemAsync (Expression<Func<T, bool>> predicate) {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T> (
                UriFactory.CreateDocumentCollectionUri (DatabaseId, CollectionId),
                new FeedOptions { MaxItemCount = 1 })
                .Where (predicate)
                .AsDocumentQuery ();

            List<T> results = new List<T> ();
            while (query.HasMoreResults) {
                results.AddRange (await query.ExecuteNextAsync<T> ());
            }

            return results.FirstOrDefault();
        }

        public async Task<Document> CreateItemAsync (T item) {
            return await client.CreateDocumentAsync (UriFactory.CreateDocumentCollectionUri (DatabaseId, CollectionId), item);
        }

        public async Task<Document> UpdateItemAsync (string id, T item) {
            return await client.ReplaceDocumentAsync (UriFactory.CreateDocumentUri (DatabaseId, CollectionId, id), item);
        }

        public async Task DeleteItemAsync (string id) {
            await client.DeleteDocumentAsync (UriFactory.CreateDocumentUri (DatabaseId, CollectionId, id));
        }

        private async Task CreateDatabaseIfNotExistsAsync () {
            try {
                await client.ReadDatabaseAsync (UriFactory.CreateDatabaseUri (DatabaseId));
            } catch (DocumentClientException e) {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    await client.CreateDatabaseAsync (new Database { Id = DatabaseId });
                } else {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync () {
            try {
                await client.ReadDocumentCollectionAsync (UriFactory.CreateDocumentCollectionUri (DatabaseId, CollectionId));
            } catch (DocumentClientException e) {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound) {
                    await client.CreateDocumentCollectionAsync (
                        UriFactory.CreateDatabaseUri (DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                } else {
                    throw;
                }
            }
        }
    }
}