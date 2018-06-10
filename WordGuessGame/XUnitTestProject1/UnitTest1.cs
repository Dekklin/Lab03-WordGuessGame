using System;
using Xunit;
using WordGuessGame;
using static WordGuessGame.Program;
using System.IO;

namespace XUnitTestProject1
{
    public class UnitTest1
    {

        [Theory]
        [InlineData(@"test.txt", "file created")]
        [InlineData(@"test2.txt", "file created")]
        public void CanCreateFile(string testPath, string expectedResult)
        {
            Assert.Equal(expectedResult, CreateFile(testPath));
            File.Delete(testPath);
        }

        [Theory]
        [InlineData("", "not a valid .txt file")]
        [InlineData("dsaj.#@$^&&$", "not a valid .txt file")]
        public void CreateThrowsException(string testPath, string expectedResult)
        {
            Assert.Equal(expectedResult, CreateFile(testPath));
        }



        [Theory]
        [InlineData(@"../../../WordList.txt", "read file")]
        [InlineData(@"asdf;oalihfoaweifh", "file does not exist")]
        public void CanReadFile(string path, string expectedResult)
        {
            Assert.Equal(expectedResult, ReadFile(path));
        }
        [Theory]
        [InlineData(@"../../../WordList.txt", "file deleted")]
        [InlineData(@"pleasefailalready.txt", "file not found")]
        public void CanDeleteFile(string path, string expectedResult)
        {
            Assert.Equal(expectedResult, DeleteFile(path));
        }

        [Theory]
        [InlineData(@"newWordPlease", "updated file")]
        [InlineData(@"pleasepasstest", "updated file")]
        public void CanUpdateFile(string word, string expectedResult)
        {
            Assert.Equal(expectedResult, UpdateFile(word));
        }
        [Theory]
        [InlineData(@"", "")]
        public void CanGuessLetter(string word, string expectedResult)
        {
            Assert.Equal(expectedResult, )
        }
    }
}
