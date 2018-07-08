using Evolent.Data;
using Evolent.Repository.TokenAuditData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Evolent.UnitTests.Repository
{
    [TestClass]
    public class AuditTrailRepositoryTest
    {
        public AuditTrailRepositoryTest()
        {
            // create some mock audit to play with
            IList<TokenAudit> tokenAudits = new List<TokenAudit>
                {
                    new TokenAudit { TransactionId = 1, TransactionDate = DateTime.Now,
                        TokenResponse = "Short description here", KeyId = "12312311",ErrorMessage=null },
                    new TokenAudit { TransactionId = 2, TransactionDate = DateTime.Now,
                        TokenResponse = "Short description here1", KeyId = "12312312",ErrorMessage="Data Error" },
                    new TokenAudit { TransactionId = 3, TransactionDate = DateTime.Now,
                        TokenResponse = "Short description here2", KeyId = "12312313",ErrorMessage=null }
                };

            // Mock the tokenAudit Repository using Moq
            Mock<ITokenAuditRepository> mockTokenAuditRepository = new Mock<ITokenAuditRepository>();

            // Return all the tokenAudits
            mockTokenAuditRepository.Setup(mr => mr.GetAll()).Returns(tokenAudits);

            // return a tokenAudit by Id
            mockTokenAuditRepository.Setup(mr => mr.Get(
                It.IsAny<int>())).Returns((int i) => tokenAudits.Where(
                x => x.TransactionId == i).Single());
            
            // Allows us to test saving a token audit
            mockTokenAuditRepository.Setup(mr => mr.AddAsync(It.IsAny<TokenAudit>())).Returns(
               async (TokenAudit target) =>
                {
                    DateTime now = DateTime.Now;

                    if (target.TransactionId.Equals(default(int)))
                    {
                        target.TransactionDate = now;
                        target.TransactionId = tokenAudits.Count() + 1;
                        tokenAudits.Add(target);
                    }
                    else
                    {
                        var original = tokenAudits.Where(
                            q => q.TransactionId == target.TransactionId).Single();

                        if (original == null)
                        {
                            return 0;
                        }

                        original.TokenResponse = target.TokenResponse;
                        original.KeyId = target.KeyId;
                        original.ErrorMessage = target.ErrorMessage;
                    
                    }

                    return 1;
                });

            // Complete the setup of our Mock token audit Repository
            this.MockTokenAuditRepository = mockTokenAuditRepository.Object;
        }

        [TestMethod]
        public void Can_Insert_Token_Audit()
        {
            // Create a new audit, not I do not supply a TransactionId
            TokenAudit newAudit = new TokenAudit
            {
                TransactionDate = DateTime.Now,
                TokenResponse = "Short description here",
                KeyId = "12312311",
                ErrorMessage = null
            };

            int auditTokenCount = this.MockTokenAuditRepository.GetAll().Count();
            Assert.AreEqual(3, auditTokenCount); // Verify the expected Number pre-insert

            // try saving our new product
            this.MockTokenAuditRepository.AddAsync(newAudit);

            // demand a recount
            auditTokenCount = this.MockTokenAuditRepository.GetAll().Count();
            Assert.AreEqual(4, auditTokenCount); // Verify the expected Number post-insert

            // verify that our new product has been saved
            TokenAudit testAudit = this.MockTokenAuditRepository.Get(4);
            Assert.IsNotNull(testAudit); // Test if null
            Assert.IsInstanceOfType(testAudit, typeof(TokenAudit)); // Test type
            Assert.AreEqual(4, testAudit.TransactionId); // Verify it has the expected productid
        }

        public readonly ITokenAuditRepository MockTokenAuditRepository;
    }
}
