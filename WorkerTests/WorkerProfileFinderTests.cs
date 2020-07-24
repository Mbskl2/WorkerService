using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Worker;
using Worker.Models;
using FluentAssertions;
using Worker.Location;
using WorkerTests.Fakes;

namespace WorkerTests
{
    [TestFixture]
    public class WorkerProfileFinderTests
    {
        private WorkerProfileFinder finder;

        [SetUp]
        public void SetUp()
        {
            finder = new WorkerProfileFinder(
                                        new FakeRepository(),
                                        new FakeAddressToCoordinatesTranslator(),
                                        new DistanceCalculator());
        }

        #region Find by skills
        [Test]
        public async Task ReturnEmpty_When_NoSkillsSpecified()
        {
            var workers = await finder.FindBySkills(ListOf());
            workers.Count().Should().Be(0);
        }

        [Test]
        public async Task ReturnEmpty_For_NotExistingSkill()
        {
            var workers = await finder.FindBySkills(ListOf("sfefsf"));
            workers.Count().Should().Be(0);
        }

        [Test]
        public async Task ReturnOne_For_Shooting()
        {
            // Given
            string skill = "Shooting";

            // When
            var workers = await finder.FindBySkills(ListOf(skill));

            // Then
            var list = workers.ToList();
            list.Count.Should().Be(1);
            AssertHasSkill(list[0], skill);
        }

        [Test]
        public async Task ReturnOne_For_LowerCaseShooting()
        {
            // Given
            string skill = "shooting";

            // When
            var workers = await finder.FindBySkills(ListOf(skill));

            // Then
            var list = workers.ToList();
            list.Count.Should().Be(1);
            AssertHasSkill(list[0], skill);
        }

        [Test]
        public async Task Return3_For_WeirdCaseSwimming()
        {
            // Given
            string skill = "sWimMIng";

            // When
            var workers = await finder.FindBySkills(ListOf(skill));

            // Then
            var list = workers.ToList();
            list.Count.Should().Be(3);
            AssertHasSkill(list[0], skill);
            AssertHasSkill(list[1], skill);
            AssertHasSkill(list[2], skill);
        }

        [Test]
        public async Task Return1_For_ClimbingRunningAndSwimming()
        {
            // Given
            var skills = new[] {"Climbing", "Running", "Swimming"};

            // When
            var workers = await finder.FindBySkills(ListOf(skills));

            // Then
            var list = workers.ToList();
            list.Count.Should().Be(1);
            AssertHasSkill(list[0], skills[0]);
            AssertHasSkill(list[0], skills[1]);
            AssertHasSkill(list[0], skills[2]);
        }

        private List<Worker.Models.Skill> ListOf(params string[] skills)
        {
            return skills.Select(x => new Skill() {Name = x}).Select(x => (Skill)x).ToList();
        }

        private void AssertHasSkill(WorkerProfile worker, string skill)
        {
            worker.Skills
                .Any(s => s.Name.Equals(skill, StringComparison.InvariantCultureIgnoreCase))
                .Should().BeTrue();
        }
        #endregion

        #region Find in vicinity

        [Test]
        public async Task ReturnEmpty_When_NoneInVicinity()
        {
            var address = new Address() {HouseNumber = "50"};
            var workers = await finder.FindInRadiusOfAddress(10.0, address);
            workers.Count().Should().Be(0);
        }

        [Test]
        public async Task Return1_When_SameAddressAndRadius0()
        {
            var address = new Address() { HouseNumber = "1" };
            var workers = await finder.FindInRadiusOfAddress(0.0, address);
            workers.Count().Should().Be(1);
        }

        [Test]
        public async Task Return2_When_Radius100()
        {
            // Given
            var address = new Address() { HouseNumber = "2.5" };

            // When
            var workers = await finder.FindInRadiusOfAddress(100.0, address);

            // Then
            var list = workers.ToList();
            list.Count.Should().Be(2);
            list[0].Address.HouseNumber.Should().Be("2");
            list[1].Address.HouseNumber.Should().Be("3");
        }

        [Test]
        public async Task ReturnAll_When_Radius5000()
        {
            var address = new Address() { HouseNumber = "20" };
            var workers = await finder.FindInRadiusOfAddress(5000.0, address);
            workers.Count().Should().Be(5);
        }

        #endregion
    }
}