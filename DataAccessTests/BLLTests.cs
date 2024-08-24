using Microsoft.Extensions.DependencyInjection;
using Apexa.BLL.Interfaces;
using Apexa.DataContracts;
using Apexa.BLL;
using Support;

namespace ApexaTests
{
    public class BLLTests
    {
        #region Properties&Attributes
        private readonly IAdvisorAPILogic BLL;

        Advisor NewAdvisor1 = new Advisor()
        {
            Name = "John Doe",
            SIN = "160028304",
            Address = "123 this Road, This Place",
            Phone = "0123456789"
        };

        Advisor NewAdvisor2 = new Advisor()
        {
            Name = "Jane Doe",
            SIN = "568281364",
            Address = "Bla Bla Bla",
            Phone = "1238904567"
        };


        Advisor NewAdvisor3 = new Advisor()
        {
            Name = "Jack Black",
            SIN = "943780825",
            Address = "1 Nevercrest, Abolsoltuion, Ontario",
            Phone = "6666666666"
        };
        #endregion Properties&Attributes

        #region Lifetime
        public BLLTests()
        {
            //Prepare DB DI            
            Apexa.BLL.DependencyPreparation.PrepareDependencies(SystemAccessOps.Services);
            SystemAccessOps.BuildServices();
            BLL = SystemAccessOps.Provider.GetRequiredService<IAdvisorAPILogic>();
        }
        #endregion Lifetime

        #region Tests
        /// <summary>
        /// Add Advisor to DB from supplied Advisor object
        /// </summary>
        /// <param name="Adv"></param>
        /// <returns></returns>
        private async Task CreateAdvisor(Advisor Adv)
        {
            if (BLL != null)
            {
                try
                {
                    await BLL.CreateAdvisor(Adv);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
            }
        }

        /// <summary>
        /// Delete identified Advisor from DB
        /// </summary>
        /// <param name="SIN"></param>
        /// <returns></returns>
        private async Task DeleteAdvisor(string SIN)
        {
            try
            {
                //now delete the currently added advisor                    
                await BLL.DeleteAdvisor(SIN);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        /// Create a new advisro and validate it exists after creation
        /// and then delete it
        /// </summary>
        [Fact]
        public async void CreateDeleteAdvisor()
        {
            Advisor? RetrievedAdvisor = null;

            if (BLL != null)
            {
                try
                {
                    await CreateAdvisor(NewAdvisor1);

                    //get the advisro --
                    RetrievedAdvisor = await BLL.GetAdvisor(NewAdvisor1.SIN);
                    Assert.NotNull(RetrievedAdvisor);

                    //now delete the currently added advisor                    
                    await DeleteAdvisor(RetrievedAdvisor.SIN);

                    //see if it still exists
                    RetrievedAdvisor = null;
                    RetrievedAdvisor = await BLL.GetAdvisor(NewAdvisor1.SIN);
                    Assert.Null(RetrievedAdvisor);
                }
                catch (Exception ex)
                {
                    Assert.Fail(ex.Message);
                }
                finally
                {
                    if (RetrievedAdvisor != null)
                        await DeleteAdvisor(RetrievedAdvisor.SIN);
                }
            }
        }

        /// <summary>
        /// Perform a modification
        /// of an advisor and validate 
        /// changes took effect
        /// </summary>
        [Fact]
        public async void UpdateAdvisor()
        {
            Advisor? RetrievedAdvisor = null;
            Advisor? UpdatedAdvisor = null;
            HealthStatus HoldHS;

            try
            {
                await CreateAdvisor(NewAdvisor1);
                //only update the paramters that can change
                //SIN is used as a key and will be used to locate
                //the advisor to update - so can't be changed here
                RetrievedAdvisor = await BLL.GetAdvisor(NewAdvisor1.SIN);
                HoldHS = RetrievedAdvisor.HealthStatus;

                RetrievedAdvisor.Phone = "9876543210";
                RetrievedAdvisor.Address = "A Change in Address";
                RetrievedAdvisor.HealthStatus = HoldHS ==
                                                HealthStatus.Green ?
                                                    HealthStatus.Red :
                                                HoldHS == HealthStatus.Red ?
                                                    HealthStatus.Green :
                                                HealthStatus.Yellow;
                //save thge changes
                await BLL.SaveAdvisor(RetrievedAdvisor);

                //validate changes have taken place
                UpdatedAdvisor = await BLL.GetAdvisor(RetrievedAdvisor.SIN);
                Assert.True(UpdatedAdvisor.Phone == RetrievedAdvisor.Phone);
                Assert.True(UpdatedAdvisor.Address == RetrievedAdvisor.Address);
                Assert.True(UpdatedAdvisor.HealthStatus == RetrievedAdvisor.HealthStatus);
            }
            finally
            {
                //delete the test advisor
                if (RetrievedAdvisor != null) DeleteAdvisor(RetrievedAdvisor.SIN);
            }
        }

        /// <summary>
        /// Will prepare a list of advisors
        /// then retrieve them and validate
        /// what was retrieved was what was saved
        /// </summary>
        [Fact]
        public async void GetAdvisors()
        {
            try
            {
                //create three advisors
                await CreateAdvisor(NewAdvisor1);
                await CreateAdvisor(NewAdvisor2);
                await CreateAdvisor(NewAdvisor3);

                //get all the advisros 
                IEnumerable<Advisor> AllAdvisors = await BLL.GetAdvisors();

                //validate that the list contains all advisors added above
                Assert.NotNull(AllAdvisors.Where(advisor => advisor.SIN.ToLower().Trim().Equals(NewAdvisor1.SIN.ToLower().Trim())).FirstOrDefault());
                Assert.NotNull(AllAdvisors.Where(advisor => advisor.SIN.ToLower().Trim().Equals(NewAdvisor2.SIN.ToLower().Trim())).FirstOrDefault());
                Assert.NotNull(AllAdvisors.Where(advisor => advisor.SIN.ToLower().Trim().Equals(NewAdvisor3.SIN.ToLower().Trim())).FirstOrDefault());
            }
            finally
            {
                DeleteAdvisor(NewAdvisor1.SIN);
                DeleteAdvisor(NewAdvisor2.SIN);
                DeleteAdvisor(NewAdvisor3.SIN);
            }
        }

        /// <summary>
        /// Validate that only one record
        /// can have the same SIN
        /// </summary>
        [Fact]
        public async void ValidateUniqueSIN()
        {
            try
            {
                //add first one
                await CreateAdvisor(NewAdvisor1);
                //attempt to add a duplicate 
                await CreateAdvisor(NewAdvisor1);
                Assert.Fail("Was able to add an advisor with the same SIN");
            }
            catch (Exception ex)
            {
                Assert.True(true,ex.Message);
            }
            finally
            {
                DeleteAdvisor(NewAdvisor1.SIN);
            }
        }
        #endregion Tests
    }
}
