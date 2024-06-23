using Microsoft.EntityFrameworkCore;
using ServerManagerCore.Interfaces;
using ServerManagerCore.Models;
using ServerManagerDAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerManagerDAL.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SessionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Session> GetSessions()
        {
            try
            {
                return _dbContext.Sessions.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve sessions", ex);
            }
        }

        public Session GetSessionById(int id)
        {
            try
            {
                return _dbContext.Sessions.FirstOrDefault(s => s.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve session", ex);
            }
        }

        public Session CreateSession(Session session)
        {
            try
            {
                _dbContext.Sessions.Add(session);
                _dbContext.SaveChanges();
                return session;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create session", ex);
            }
        }

        public Session UpdateSession(Session session)
        {
            try
            {
                var existingSession = _dbContext.Sessions.FirstOrDefault(s => s.Id == session.Id);
                if (existingSession != null)
                {
                    existingSession.Title = session.Title;
                    existingSession.Description = session.Description;
                    existingSession.StartTime = session.StartTime;
                    existingSession.EndTime = session.EndTime;
                    existingSession.ServerId = session.ServerId;
                    _dbContext.SaveChanges();
                }

                return existingSession;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update session", ex);
            }
        }

        public bool DeleteSession(int id)
        {
            try
            {
                var sessionToDelete = _dbContext.Sessions.FirstOrDefault(s => s.Id == id);
                if (sessionToDelete != null)
                {
                    _dbContext.Sessions.Remove(sessionToDelete);
                    _dbContext.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete session", ex);
            }
        }

        public void AddUserToSession(int sessionId, int userId)
        {
            try
            {
                var session = _dbContext.Sessions.Include(s => s.SessionUsers).FirstOrDefault(s => s.Id == sessionId);
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

                if (session == null || user == null)
                {
                    throw new Exception("Session or User not found.");
                }

                var sessionUser = new SessionUser { SessionId = sessionId, UserId = userId };
                _dbContext.SessionUsers.Add(sessionUser);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to add user to session", ex);
            }
        }

        public void RemoveUserFromSession(int sessionId, int userId)
        {
            try
            {
                var sessionUser = _dbContext.SessionUsers.FirstOrDefault(su => su.SessionId == sessionId && su.UserId == userId);

                if (sessionUser == null)
                {
                    throw new Exception("Session or User not found.");
                }

                _dbContext.SessionUsers.Remove(sessionUser);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to remove user from session", ex);
            }
        }

        public List<User> GetAppliedUsers(int sessionId)
        {
            try
            {
                return _dbContext.SessionUsers
                    .Where(su => su.SessionId == sessionId)
                    .Select(su => su.User)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve applied users", ex);
            }
        }
    }
}
